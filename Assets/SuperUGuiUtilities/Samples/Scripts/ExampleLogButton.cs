using UnityEngine;

namespace SuperUGuiUtilities.Samples {
	public class ExampleLogButton : ButtonBehavior {
		[SerializeField]
		private string logMessage = "Click!";

		protected override void OnClick() => Debug.Log(logMessage);
	}
}
