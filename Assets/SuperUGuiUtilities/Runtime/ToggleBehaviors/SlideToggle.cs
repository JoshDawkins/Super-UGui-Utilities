using System;
using DG.Tweening;
using UnityEngine;

namespace SuperUGuiUtilities {
	[ExecuteAlways]
	public class SlideToggle : AnimationToggleBase {
		[SerializeField]
		private ElementConfig[] targets;

		protected override Tween Animate(bool isOn, float duration) {
			Sequence seq = DOTween.Sequence();
			targets.ForEach(target => seq.Join(target.Animate(isOn, duration)));

			return seq;
		}
		protected override void SetWithoutAnimation(bool isOn)
			=> targets.ForEach(target => target.SetWithoutAnimation(isOn));


		[Serializable]
		public class ElementConfig {
			[SerializeField]
			private RectTransform target;
			[SerializeField]
			private bool controlAnchors = false;

			[SerializeField, Header("On Settings")]
			private Vector2 onAnchoredPos;
			[SerializeField, ShowIf(nameof(controlAnchors))]
			private Vector2 onAnchorMin, onAnchorMax, onPivot;

			[SerializeField, Header("Off Settings")]
			private Vector2 offAnchoredPos;
			[SerializeField, ShowIf(nameof(controlAnchors))]
			private Vector2 offAnchorMin, offAnchorMax, offPivot;


			public Tween Animate(bool isOn, float duration) {
				if (target == null)
					return null;
				
				Tween result = target.DOAnchorPos(isOn ? onAnchoredPos : offAnchoredPos, duration);

				if (controlAnchors)
					result = DOTween.Sequence(target)
						.Join(target.DOAnchorMin(isOn ? onAnchorMin : offAnchorMin, duration))
						.Join(target.DOAnchorMax(isOn ? onAnchorMax : offAnchorMax, duration))
						.Join(target.DOPivot(isOn ? onPivot : offPivot, duration))
						.Join(result);

				return result;
			}
			public void SetWithoutAnimation(bool isOn) {
				if (target == null)
					return;
				
				if (controlAnchors) {
					target.anchorMin = isOn ? onAnchorMin : offAnchorMin;
					target.anchorMax = isOn ? onAnchorMax : offAnchorMax;
					target.pivot = isOn ? onPivot : offPivot;
				}

				target.anchoredPosition = isOn ? onAnchoredPos : offAnchoredPos;
			}
		}
	}
}
