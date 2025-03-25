namespace SuperUGuiUtilities.Samples {
	public class SimpleThemeExampleDropdown : TypeDropdown<SimpleThemeExample> {
		protected override string GetOptionTitle(SimpleThemeExample option)
			=> option.ThemeName;

#if UNITY_EDITOR
		protected override void OnValidate() {
			base.OnValidate();

			if (SimpleThemeExampleManager.Instance == null)
				return;
			
			options.Clear();
			options.AddRange(SimpleThemeExampleManager.Instance.ValidThemes);
		}
#endif
	}
}
