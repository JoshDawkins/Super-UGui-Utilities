using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace SuperUGuiUtilities.Samples {
	[CreateAssetMenu(menuName = "Super UGUI Samples/Full Theme Example")]
	public class FullThemeExample : ThemeDefinitionBase<FullThemeStyleId, FullThemeExampleStyleBase> {
		[SerializeField]
		private FullThemeRectStyle containerStyle = new(FullThemeStyleId.Container);
		[SerializeField]
		private FullThemeImageStyle backgroundStyle = new(FullThemeStyleId.Background);
		[SerializeField]
		private FullThemeImageStyle iconStyle = new(FullThemeStyleId.Icon);
		[SerializeField]
		private FullThemeTextStyle headerStyle = new(FullThemeStyleId.Header);
		[SerializeField]
		private FullThemeTextStyle descriptionStyle = new(FullThemeStyleId.Description);

		private Dictionary<FullThemeStyleId, FullThemeExampleStyleBase> styles;
		private void OnEnable() {
			styles = new() {
				{ FullThemeStyleId.Container, containerStyle },
				{ FullThemeStyleId.Background, backgroundStyle },
				{ FullThemeStyleId.Icon, iconStyle },
				{ FullThemeStyleId.Header, headerStyle },
				{ FullThemeStyleId.Description, descriptionStyle },
			};

			_readOnlyStyles = styles.Values.ToList().AsReadOnly();
		}

		private ReadOnlyCollection<FullThemeExampleStyleBase> _readOnlyStyles;
		public override ReadOnlyCollection<FullThemeExampleStyleBase> AllStyles => _readOnlyStyles;

		public override FullThemeExampleStyleBase GetStyle(FullThemeStyleId styleId)
			=> styles.TryGetValue(styleId, out FullThemeExampleStyleBase style) ? style : null;
	}
}
