using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	public abstract class ToggleBehavior : UIBehaviour {
		[field: SerializeField]
		public Toggle TargetToggle { get; private set; }
		[field: SerializeField, ShowIf(nameof(triggerOnEnableHandling), TriggerOnEnableHandling.Choose)]
		public bool TriggerOnEnable { get; private set; }
		protected virtual TriggerOnEnableHandling triggerOnEnableHandling => TriggerOnEnableHandling.Choose;


		protected override void OnEnable() {
			TargetToggle.TryAddOnValueChangedListener(OnToggle);

			ValidateTriggerOnEnable();
			if (TriggerOnEnable && TargetToggle.TryGetIsOn(out bool isOn))
				OnToggle(isOn);
		}
		protected override void OnDisable() => TargetToggle.TryRemoveOnValueChangedListener(OnToggle);

		protected abstract void OnToggle(bool isOn);

		private void ValidateTriggerOnEnable()
			=> TriggerOnEnable = triggerOnEnableHandling switch {
				TriggerOnEnableHandling.Always => true,
				TriggerOnEnableHandling.Never => false,
				_ => TriggerOnEnable
			};

#if UNITY_EDITOR
		protected override void OnValidate() {
			if (TargetToggle == null)
				TargetToggle = GetComponent<Toggle>();
			ValidateTriggerOnEnable();
		}
#endif


		protected enum TriggerOnEnableHandling : byte {
			Choose,
			Always,
			Never,
		}
	}
}
