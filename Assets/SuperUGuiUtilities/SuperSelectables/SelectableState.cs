using UnityEngine.UI;

namespace SuperUGuiUtilities {
	public enum SelectableState {
		/// <summary>The default state of a selectable</summary>
		Normal,

		/// <summary>The state when the pointer is over the selectable, but not pressed</summary>
		Highlighted,

		/// <summary>The state while the pointer is over the selectable and pressed</summary>
		Pressed,

		/// <summary>The state of the current active selectable, while not highlighted, pressed, or disabled</summary>
		Selected,

		/// <summary>The state of the selectable while it cannot be selected (<see cref="Selectable.IsInteractable"/> is false)</summary>
		Disabled,
	}
}
