using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	public class AdvancedCooldownButton : ButtonBehavior {
		[field: SerializeField]
		public float CooldownS { get; private set; }
		[SerializeField]
		private Slider progressFill;

		private Tween cooldownTween;
		private float elapsedFallback = 0;


		protected override void Start() {
			base.Start();
			progressFill.TrySetRange(0, CooldownS);
			progressFill.TrySetValue(0);
		}

		public void EndCooldown() => cooldownTween?.Kill(true);

		protected override void OnClick() {
			targetButton.TrySetInteractable(false);
			cooldownTween = progressFill == null
				? (Tween)DOTween.To(() => elapsedFallback, (x) => elapsedFallback = x, CooldownS, CooldownS)
				: DOTween.To(() => progressFill.value, (x) => progressFill.value = x, CooldownS, CooldownS);
			cooldownTween.OnComplete(OnCooldownComplete);
		}
		private void OnCooldownComplete() {
			cooldownTween = null;
			elapsedFallback = 0;
			progressFill.TrySetValue(0);
			targetButton.TrySetInteractable(true);
		}
	}
}
