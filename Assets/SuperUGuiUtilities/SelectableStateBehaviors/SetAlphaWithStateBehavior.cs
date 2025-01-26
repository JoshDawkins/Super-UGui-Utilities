using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	public class SetAlphaWithStateBehavior : SelectableStateBehavior {
		[SerializeField]
		private Config[] targetElements;

		protected override void OnStateChanged(SelectableState state, bool instant)
			=> targetElements.ForEach(conf => conf.SetState(state));


		[Serializable]
		private class Config {
			[SerializeField]
			private TargetType targetType;

			[SerializeField, AllowNesting, Required, Label("Target")]
			[ShowIf(nameof(targetType), TargetType.CanvasGroup)]
			private CanvasGroup targetGroup;

			[SerializeField, AllowNesting, Required, Label("Target")]
			[ShowIf(nameof(targetType), TargetType.Graphic)]
			private Graphic targetGraphic;

			[SerializeField, Range(0, 1)]
			private float normalAlpha = 1,
				highlightedAlpha = 1,
				pressedAlpha = 1,
				selectedAlpha = 1,
				disabledAlpha = 1;

			public void SetState(SelectableState state) {
				float newAlpha = state switch {
					SelectableState.Normal => normalAlpha,
					SelectableState.Highlighted => highlightedAlpha,
					SelectableState.Pressed => pressedAlpha,
					SelectableState.Selected => selectedAlpha,
					SelectableState.Disabled => disabledAlpha,
					_ => normalAlpha
				};

				switch (targetType) {
					case TargetType.CanvasGroup:
						if (targetGroup != null)
							targetGroup.alpha = newAlpha;
						break;

					case TargetType.Graphic:
						if (targetGraphic != null)
							targetGraphic.CrossFadeAlpha(newAlpha, 0, true);
						break;
				}
			}


			private enum TargetType : byte {
				CanvasGroup, Graphic,
			}
		}
	}
}
