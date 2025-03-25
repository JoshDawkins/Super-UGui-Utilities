using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
		private void InitStyles() => styles ??= new() {
			{ FullThemeStyleId.Container, containerStyle },
			{ FullThemeStyleId.Background, backgroundStyle },
			{ FullThemeStyleId.Icon, iconStyle },
			{ FullThemeStyleId.Header, headerStyle },
			{ FullThemeStyleId.Description, descriptionStyle },
		};


		public override ReadOnlyCollection<FullThemeExampleStyleBase> AllStyles {
			get {
				InitStyles();
				return styles.Values.ToList().AsReadOnly();
			}
		}

		public override FullThemeExampleStyleBase GetStyle(FullThemeStyleId styleId) {
			InitStyles();
			return styles.TryGetValue(styleId, out FullThemeExampleStyleBase style) ? style : null;
		}
	}


	public enum FullThemeStyleId {
		Container,
		Background,
		Icon,
		Header,
		Description,
	}


	public abstract class FullThemeExampleStyleBase : ThemeStyleBase<FullThemeStyleId> {
        protected FullThemeExampleStyleBase(FullThemeStyleId id)
        {
			StyleId = id;
			_preventEditing = true;
        }
    }

	[Serializable]
	public class FullThemeRectStyle : FullThemeExampleStyleBase, IProvidesThemeFor<RectTransform> {
		[field: SerializeField]
		public Vector2 AnchoredPosition { get; private set; }
		[field: SerializeField]
		public Vector2 SizeDelta { get; private set; }
		[field: SerializeField]
		public Vector2 AnchorMin { get; private set; }
		[field: SerializeField]
		public Vector2 AnchorMax { get; private set; }
		[field: SerializeField]
		public Vector2 Pivot { get; private set; }


		public FullThemeRectStyle(FullThemeStyleId id) : base(id) { }

		public void ApplyThemeTo(RectTransform target) {
			if (target == null)
				return;

			target.anchorMin = AnchorMin;
			target.anchorMax = AnchorMax;
			target.pivot = Pivot;
			target.anchoredPosition = AnchoredPosition;
			target.sizeDelta = SizeDelta;
		}
	}

	[Serializable]
	public class FullThemeImageStyle : FullThemeExampleStyleBase, IProvidesThemeFor<Image> {
		[field: SerializeField]
		public Sprite Sprite { get; private set; }
		[field: SerializeField]
		public Color Color { get; private set; }


		public FullThemeImageStyle(FullThemeStyleId id) : base(id) { }

		public void ApplyThemeTo(Image target) {
			if (target == null)
				return;

			target.sprite = Sprite;
			target.color = Color;
		}
	}

	[Serializable]
	public class FullThemeTextStyle : FullThemeExampleStyleBase, IProvidesThemeFor<TextMeshProUGUI> {
		[field: SerializeField]
		public float FontSize { get; private set; }
		[field: SerializeField]
		public FontStyles FontStyle { get; private set; }
		[field: SerializeField]
		public Color FontColor { get; private set; }


		public FullThemeTextStyle(FullThemeStyleId id) : base(id) { }

		public void ApplyThemeTo(TextMeshProUGUI target) {
			if (target == null)
				return;

			target.fontSize = FontSize;
			target.enableAutoSizing = false;
			target.fontStyle = FontStyle;
			target.color = FontColor;
		}
	}
}
