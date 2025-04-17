using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	public class AdvancedCooldownButton : ButtonBehavior {
		[field: SerializeField]
		public float CooldownS { get; private set; }
		[field: SerializeField]
		public Ease Easing { get; private set; }
		[SerializeField]
		private Slider progressFill;
		[SerializeField]
		private bool startFilled;

		private Tween cooldownTween;
		private float elapsedFallback = 0;


		protected override void Start() {
			base.Start();
			progressFill.TrySetRange(0, CooldownS);
			progressFill.TrySetValue(startFilled ? CooldownS : 0);
		}

		public void EndCooldown() => cooldownTween?.Kill(true);

		protected override void OnClick() {
			targetButton.TrySetInteractable(false);
			cooldownTween = progressFill == null
				? (Tween)DOTween.To(() => elapsedFallback, (x) => elapsedFallback = x, CooldownS, CooldownS)
				: progressFill.DOValue(CooldownS, CooldownS).ChangeStartValue(0);
			cooldownTween.SetEase(Easing).OnComplete(OnCooldownComplete);
		}
		private void OnCooldownComplete() {
			cooldownTween = null;
			elapsedFallback = 0;
			progressFill.TrySetValue(startFilled ? CooldownS : 0);
			targetButton.TrySetInteractable(true);
		}
	}
}
