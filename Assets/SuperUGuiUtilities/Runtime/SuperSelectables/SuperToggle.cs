using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	[AddComponentMenu("UI/Super Selectables/Super Toggle")]
	public class SuperToggle : Toggle, ISuperSelectable {
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
		[MenuItem("CONTEXT/Toggle/Convert to Super Toggle")]
		private static void ConvertFromStandardToggle(MenuCommand cmd) {
			var tog = cmd.context as Toggle;
			if (tog == null || !EditorUtility.DisplayDialog("Convert to Super Toggle?",
				"Converting a standard Toggle component to a SuperToggle will maintain all standard " +
				"Toggle properties, but properties from any subclass of Toggle and any serialized references " +
				"other components hold to this component will be lost. Proceed anyways?",
				"Yes, Convert", "No, Don't Convert"))
				return;

			var toggleTransition = tog.toggleTransition;
			var graphic = tog.graphic;
			var group = tog.group;
			var onValueChanged = tog.onValueChanged;
			var isOn = tog.isOn;

			SuperToggle super = ISuperSelectable.FromStandardSelectable<Toggle, SuperToggle>(tog);

			super.toggleTransition = toggleTransition;
			super.graphic = graphic;
			super.group = group;
			super.onValueChanged = onValueChanged;
			super.isOn = isOn;
		}
#endif
	}
}
