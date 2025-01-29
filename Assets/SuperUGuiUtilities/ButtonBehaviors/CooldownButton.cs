using UnityEngine;

namespace SuperUGuiUtilities {
	public class CooldownButton : ButtonBehavior {
		[field: SerializeField, Min(0.01f)]
		public float CooldownS { get; set; }
		[field: SerializeField]
		public bool IsRealtime { get; set; } = true;

		private Coroutine cooldownRoutine;


		protected override void OnClick() {
			targetButton.TrySetInteractable(false);
			cooldownRoutine = IsRealtime
				? this.InvokeAfterSecondsRealtime(EndCooldown, CooldownS)
				: this.InvokeAfterSeconds(EndCooldown, CooldownS);
		}

		public void EndCooldown() {
			if (cooldownRoutine != null) {
				StopCoroutine(cooldownRoutine);
				cooldownRoutine = null;

				targetButton.TrySetInteractable(true);
			}
		}
	}
}
