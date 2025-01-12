using System;

namespace SuperUGuiUtilities {
	public interface ISuperSelectable {
		SelectableState CurrentState { get; }

		event Action<SelectableState, bool> OnStateChanged;
	}
}
