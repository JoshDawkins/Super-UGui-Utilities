using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	public class AdvancedCooldownButton : ButtonBehavior {
		[field: SerializeField]
		public float CooldownS { get; private set; }
		[SerializeField]
		private Slider progressFill;

		private Coroutine cooldownRoutine;


		protected override void Start() {
			base.Start();
			progressFill.TrySetRange(0, CooldownS);
			progressFill.TrySetValue(0);
		}

		protected override void OnClick() => cooldownRoutine = StartCoroutine(CooldownRoutine());

		private IEnumerator CooldownRoutine() {
			targetButton.TrySetInteractable(false);
			float timePassed = 0;

			while (timePassed < CooldownS) {
				progressFill.TrySetValue(timePassed);
				yield return null;
				timePassed += Time.deltaTime;
			}

			progressFill.TrySetValue(0);
			targetButton.TrySetInteractable(true);
		}
	}
}
