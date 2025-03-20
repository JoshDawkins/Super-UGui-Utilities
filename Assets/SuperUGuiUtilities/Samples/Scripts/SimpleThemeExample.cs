using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities.Samples {
	[CreateAssetMenu(menuName = "Super UGUI Samples/Simple Theme Example")]
	public class SimpleThemeExample : SimpleThemeDefinitionBase<SimpleThemeExampleId, SimpleThemeStyle, Image> {

	}

	public enum SimpleThemeExampleId {
		StyleA,
		StyleB,
		StyleC,
	}

	[Serializable]
	public class SimpleThemeStyle : ThemeStyleBase<SimpleThemeExampleId>, IProvidesThemeFor<Image> {
		[field: SerializeField]
		public Color Color { get; private set; } = Color.white;

		public void ApplyThemeTo(Image target) {
			if (target != null)
				target.color = Color;
		}
	}
}
