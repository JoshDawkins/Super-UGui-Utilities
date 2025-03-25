namespace SuperUGuiUtilities.Samples {
	public class FullThemeExampleDropdown : TypeDropdown<FullThemeExample> {
		protected override string GetOptionTitle(FullThemeExample option)
			=> option.ThemeName;

#if UNITY_EDITOR
		protected override void OnValidate() {
			base.OnValidate();

			if (FullThemeExampleManager.Instance == null)
				return;

			options.Clear();
			options.AddRange(FullThemeExampleManager.Instance.ValidThemes);
		}
#endif
	}
}
