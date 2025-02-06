using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	public abstract class ButtonBehavior : UIBehaviour {
		[SerializeField, Required]
		protected Button targetButton;

		protected override void OnEnable() => targetButton.TryAddClickListener(OnClick);
		protected override void OnDisable() => targetButton.TryRemoveClickListener(OnClick);

		protected abstract void OnClick();

#if UNITY_EDITOR
		protected override void OnValidate() {
			if (targetButton == null)
				targetButton = GetComponent<Button>();
		}
#endif
	}
}
