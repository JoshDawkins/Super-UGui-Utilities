using System;
using UnityEngine;

namespace SuperUGuiUtilities {
	public class SetCanvasGroupAlphaWithStateBehavior : SelectableStateBehavior {
		[SerializeField]
		private Config[] targetElements;

		protected override void OnStateChanged(SelectableState state, bool instant)
			=> targetElements.ForEach(conf => conf.SetState(state));


		[Serializable]
		private class Config {
			[SerializeField]
			private CanvasGroup target;

			[SerializeField, Range(0, 1)]
			private float normalAlpha = 1,
				highlightedAlpha = 1,
				pressedAlpha = 1,
				selectedAlpha = 1,
				disabledAlpha = 1;

			public void SetState(SelectableState state) {
				if (target != null)
					target.alpha = (float)(state switch {
						SelectableState.Normal => normalAlpha,
						SelectableState.Highlighted => highlightedAlpha,
						SelectableState.Pressed => pressedAlpha,
						SelectableState.Selected => selectedAlpha,
						SelectableState.Disabled => disabledAlpha,
						_ => normalAlpha
					});
			}
		}
	}
}
