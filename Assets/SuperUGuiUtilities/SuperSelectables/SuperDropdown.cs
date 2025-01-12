using System;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	public class SuperDropdown : Dropdown, ISuperSelectable {
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
	}
}
