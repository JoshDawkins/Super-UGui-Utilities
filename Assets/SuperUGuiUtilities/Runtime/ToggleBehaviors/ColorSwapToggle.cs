using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	[ExecuteAlways]
	public class ColorSwapToggle : AnimationToggleBase {
		[SerializeField]
		private GraphicConfig[] targets;

		protected override Tween Animate(bool isOn, float duration) {
			Sequence seq = DOTween.Sequence();
			targets.ForEach(target => seq.Join(target.Animate(isOn, duration)));

			return seq;
		}
		protected override void SetWithoutAnimation(bool isOn)
			=> targets.ForEach(target => target.SetWithoutAnimation(isOn));


		[Serializable]
		private class GraphicConfig {
			[SerializeField, AllowNesting, Required]
			private Graphic target;
			[SerializeField]
			private Color onColor = Color.white,
				offColor = Color.white;

			public Tween Animate(bool isOn, float duration)
				=> target.DOColor(isOn ? onColor : offColor, duration);

			public void SetWithoutAnimation(bool isOn)
				=> target.color = isOn ? onColor : offColor;
		}
	}
}
