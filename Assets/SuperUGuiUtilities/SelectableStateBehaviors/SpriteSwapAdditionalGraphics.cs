using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	[ExecuteAlways]
	public class SpriteSwapAdditionalGraphics : SelectableStateBehavior {
		[SerializeField]
		private GraphicHandler[] targetGraphics;

		protected override void OnStateChanged(SelectableState state, bool instant)
			=> targetGraphics.ForEach(graphic => graphic.DoTransition(state));


		[Serializable]
		private class GraphicHandler {
			[SerializeField, Required]
			private Image target;
			[SerializeField]
			private SpriteState sprites;

			public void DoTransition(SelectableState state)
				=> sprites.ApplyStateTo(target, state);
		}
	}
}
