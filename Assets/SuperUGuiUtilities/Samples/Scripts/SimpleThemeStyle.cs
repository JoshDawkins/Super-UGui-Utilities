using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities.Samples {
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
