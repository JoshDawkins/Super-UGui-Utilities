namespace SuperUGuiUtilities.Samples {
	public class SimpleThemeExampleDropdown : TypeDropdown<SimpleThemeExample> {
		protected override string GetOptionTitle(SimpleThemeExample option)
			=> option.ThemeName;
	}
}
