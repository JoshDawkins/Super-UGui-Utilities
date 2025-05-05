using System;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace SuperUGuiUtilities {
	[AddComponentMenu("UI/Super Selectables/Super Dropdown")]
	public class SuperDropdown : TMP_Dropdown, ISuperSelectable {
		public SelectableState CurrentState { get; private set; }

		public event Action<SelectableState, bool> OnStateChanged;


		protected override void DoStateTransition(SelectionState state, bool instant) {
			base.DoStateTransition(state, instant);

			CurrentState = state switch {
				SelectionState.Highlighted => SelectableState.Highlighted,
				SelectionState.Pressed => SelectableState.Pressed,
				SelectionState.Selected => SelectableState.Selected,
				SelectionState.Disabled => SelectableState.Disabled,
				_ => SelectableState.Normal
			};
			OnStateChanged?.Invoke(CurrentState, instant);
		}

#if UNITY_EDITOR
		[MenuItem("CONTEXT/TMP_Dropdown/Convert to Super Dropdown")]
		private static void ConvertFromStandardDropdown(MenuCommand cmd) {
			var dd = cmd.context as TMP_Dropdown;
			if (dd == null || !EditorUtility.DisplayDialog("Convert to Super Dropdown?",
				"Converting a standard TMP_Dropdown component to a SuperDropdown will maintain all standard " +
				"TMP_Dropdown properties, but properties from any subclass of TMP_Dropdown and any serialized references " +
				"other components hold to this component will be lost. Proceed anyways?",
				"Yes, Convert", "No, Don't Convert"))
				return;

			var template = dd.template;
			var captionText = dd.captionText;
			var captionImage = dd.captionImage;
			var placeholder = dd.placeholder;
			var itemText = dd.itemText;
			var itemImage = dd.itemImage;
			var options = dd.options;
			var onValueChanged = dd.onValueChanged;
			var alphaFadeSpeed = dd.alphaFadeSpeed;
			var value = dd.value;
			var multiSelect = dd.MultiSelect;

			SuperDropdown super = ISuperSelectable.FromStandardSelectable<TMP_Dropdown, SuperDropdown>(dd);

			super.template = template;
			super.captionText = captionText;
			super.captionImage = captionImage;
			super.placeholder = placeholder;
			super.itemText = itemText;
			super.itemImage = itemImage;
			super.options = options;
			super.onValueChanged = onValueChanged;
			super.alphaFadeSpeed = alphaFadeSpeed;
			super.value = value;
			super.MultiSelect = multiSelect;
		}
#endif
	}
}
