using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	[ExecuteAlways]
	public class ColorTintAdditionalGraphics : SelectableStateBehavior {
		[SerializeField]
		private GraphicHandler[] targetGraphics;

		protected override void OnStateChanged(SelectableState state, bool instant)
			=> targetGraphics.ForEach(graphic => graphic.DoTransition(state, instant));


		[Serializable]
		private class GraphicHandler {
			[SerializeField]
			private Graphic target;
			[SerializeField]
			private ColorBlock colors = ColorBlock.defaultColorBlock;

			public void DoTransition(SelectableState state, bool instant)
				=> colors.ApplyStateTo(target, state, instant);
		}
	}
}
