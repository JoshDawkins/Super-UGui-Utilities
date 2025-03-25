using UnityEngine;
using UnityEngine.EventSystems;

namespace SuperUGuiUtilities.Samples {
	public class SelectFullTheme : UIBehaviour {
		[SerializeField]
		private FullThemeExampleDropdown dropdown;

		protected override void OnEnable() {
			base.OnEnable();

			dropdown.OnOptionChosen += OnOptionChosen;
		}
		protected override void OnDisable() {
			base.OnDisable();

			dropdown.OnOptionChosen -= OnOptionChosen;
		}

		private void OnOptionChosen(FullThemeExample theme)
			=> FullThemeExampleManager.Instance.TrySetCurrentTheme(theme);

#if UNITY_EDITOR
		protected override void OnValidate() {
			base.OnValidate();

			if (dropdown == null)
				dropdown = GetComponent<FullThemeExampleDropdown>();
		}
#endif
	}
}
