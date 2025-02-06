using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	public abstract class SelectableStateBehavior : UIBehaviour {
		[SerializeField, Required, Implements(typeof(ISuperSelectable))]
		protected Selectable targetSelectable;
		protected ISuperSelectable Super => targetSelectable as ISuperSelectable;

		protected override void OnEnable() => Super.TryAddStateChangedListener(OnStateChanged);
		protected override void OnDisable() => Super.TryRemoveStateChangedListener(OnStateChanged);

		protected abstract void OnStateChanged(SelectableState state, bool instant);

#if UNITY_EDITOR
		protected override void OnValidate() {
			if (targetSelectable == null)
				targetSelectable = GetComponent<Selectable>();
		}
#endif
	}
}
