using System;
using UnityEngine;

namespace SuperUGuiUtilities {
	public class ActivateGameObjectsButton : ButtonBehavior {
		[SerializeField]
		private Config[] targets;

		protected override void OnClick() => targets.ForEach(t => t.SetActive());


		[Serializable]
		private struct Config {
			[SerializeField]
			private GameObject target;
			[SerializeField]
			private bool activate;

			public void SetActive() => target.TrySetActive(activate);
		}
	}
}
