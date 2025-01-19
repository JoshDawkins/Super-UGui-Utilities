using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	[AddComponentMenu("UI/Super Selectables/Super Button")]
	public class SuperButton : Button, ISuperSelectable {
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
		[MenuItem("CONTEXT/Button/Convert to Super Button")]
		private static void ConvertFromStandardButton(MenuCommand cmd) {
			var btn = cmd.context as Button;
			if (btn == null || !EditorUtility.DisplayDialog("Convert to Super Button?",
				"Converting a standard Button component to a SuperButton will maintain all standard " +
				"Button properties, but properties from any subclass of Button and any serialized references " +
				"other components hold to this component will be lost. Proceed anyways?",
				"Yes, Convert", "No, Don't Convert"))
				return;

			var onClick = btn.onClick;
			SuperButton super = ISuperSelectable.FromStandardSelectable<Button, SuperButton>(btn);
			super.onClick = onClick;
		}
#endif
	}
}
