using UnityEngine;
using UnityEngine.EventSystems;

namespace SuperUGuiUtilities.Samples {
	public class SelectSimpleTheme : UIBehaviour {
		[SerializeField]
		private SimpleThemeExampleDropdown dropdown;

		protected override void OnEnable() {
			base.OnEnable();

			dropdown.OnOptionChosen += OnOptionChosen;
		}
		protected override void OnDisable() {
			base.OnDisable();

			dropdown.OnOptionChosen -= OnOptionChosen;
		}

		private void OnOptionChosen(SimpleThemeExample theme)
			=> SimpleThemeExampleManager.Instance.TrySetCurrentTheme(theme);

#if UNITY_EDITOR
		protected override void OnValidate() {
			base.OnValidate();

			if (dropdown == null)
				dropdown = GetComponent<SimpleThemeExampleDropdown>();
		}
#endif
	}
}
