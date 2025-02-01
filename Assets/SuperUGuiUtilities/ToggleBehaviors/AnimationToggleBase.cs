using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace SuperUGuiUtilities {
	public abstract class AnimationToggleBase : ToggleBehavior {
		[field: SerializeField, Min(0)]
		public float AnimationDurationS { get; set; }
		private bool shouldAnimate => AnimationDurationS > 0;

		[field: SerializeField, ShowIf(nameof(shouldAnimate))]
		public Ease Easing { get; set; }

		protected override TriggerOnEnableHandling triggerOnEnableHandling => TriggerOnEnableHandling.Always;
		private Tween tween;


		protected override sealed void OnToggle(bool isOn) {
			EndAnimation();

			if (shouldAnimate && Application.isPlaying)
				tween = Animate(isOn, AnimationDurationS).SetEase(Easing);
			else
				SetWithoutAnimation(isOn);
		}
		public void EndAnimation() => tween?.Kill(true);

		protected abstract Tween Animate(bool isOn, float duration);
		protected abstract void SetWithoutAnimation(bool isOn);
	}
}
