using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	public class InteractabilityToggle : ToggleBehavior {
		[SerializeField]
		private bool invert = false;
		[SerializeField]
		private Selectable[] targetSelectables;
		[SerializeField]
		private CanvasGroup[] targetCanvasGroups;

		protected override TriggerOnEnableHandling triggerOnEnableHandling => TriggerOnEnableHandling.Always;


		protected override void OnToggle(bool isOn) {
			if (invert)
				isOn = !isOn;

			targetSelectables.ForEach(target => target.interactable = isOn);
			targetCanvasGroups.ForEach(target => target.interactable = isOn);
		}
	}
}
