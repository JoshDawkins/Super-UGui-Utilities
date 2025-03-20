using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SuperUGuiUtilities {
	public abstract class DropdownBehavior : UIBehaviour {
		[SerializeField]
		protected TMP_Dropdown targetDropdown;

		protected override void OnEnable() => targetDropdown.TryAddValueChangedListener(OnOptionSelected);
		protected override void OnDisable() => targetDropdown.TryAddValueChangedListener(OnOptionSelected);

		protected abstract void OnOptionSelected(int index);

#if UNITY_EDITOR
		protected override void OnValidate() {
			if (targetDropdown == null)
				targetDropdown = GetComponent<TMP_Dropdown>();
		}
#endif
	}
}
