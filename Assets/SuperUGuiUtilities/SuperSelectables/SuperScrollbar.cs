using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	[AddComponentMenu("UI/Super Selectables/Super Scrollbar")]
	public class SuperScrollbar : Scrollbar, ISuperSelectable {
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
		[MenuItem("CONTEXT/Scrollbar/Convert to Super Scrollbar")]
		private static void ConvertFromStandardScrollbar(MenuCommand cmd) {
			var bar = cmd.context as Scrollbar;
			if (bar == null || !EditorUtility.DisplayDialog("Convert to Super Scrollbar?",
				"Converting a standard Scrollbar component to a SuperScrollbar will maintain all standard " +
				"Scrollbar properties, but properties from any subclass of Scrollbar and any serialized references " +
				"other components hold to this component will be lost. Proceed anyways?",
				"Yes, Convert", "No, Don't Convert"))
				return;

			var handleRect = bar.handleRect;
			var direction = bar.direction;
			var value = bar.value;
			var size = bar.size;
			var numberOfSteps = bar.numberOfSteps;
			var onValueChanged = bar.onValueChanged;

			SuperScrollbar super = ISuperSelectable.FromStandardSelectable<Scrollbar, SuperScrollbar>(bar);

			super.handleRect = handleRect;
			super.direction = direction;
			super.value = value;
			super.size = size;
			super.numberOfSteps = numberOfSteps;
			super.onValueChanged = onValueChanged;
		}
#endif
	}
}
