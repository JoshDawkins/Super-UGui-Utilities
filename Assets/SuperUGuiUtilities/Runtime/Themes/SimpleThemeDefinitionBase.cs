using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace SuperUGuiUtilities {
	public abstract class SimpleThemeDefinitionBase<TId, TStyle, TTarget> : ThemeDefinitionBase<TId, TStyle>
	where TId : struct, Enum
	where TStyle : ThemeStyleBase<TId>, IProvidesThemeFor<TTarget>
	where TTarget : UnityEngine.Object {
		[SerializeField]
		protected List<TStyle> styles;

		public override ReadOnlyCollection<TStyle> AllStyles => styles.AsReadOnly();
		public override TStyle GetStyle(TId styleId) => styles.FirstOrDefault(s => s.StyleId.Equals(styleId));

#if UNITY_EDITOR
		//This ensures that we always have a style for each ID, even when the enum definition is changed.
		//  Only applied in editor, since enum definitions can't change at runtime and values are serialized.
		private void OnEnable() {
			Type styleType = typeof(TStyle);
			TId[] ids = (TId[])Enum.GetValues(typeof(TId));

			styles.RemoveAll(style => !ids.Contains(style.StyleId));

			foreach (TId id in ids) {
				if (styles.Find(style => style.StyleId.Equals(id)) != null)
					continue;

				TStyle style = (TStyle)Activator.CreateInstance(styleType);
				style.StyleId = id;
				style._preventEditing = true;
				styles.Add(style);
			}

			styles = styles.OrderBy(style => style.StyleId).ToList();
		}
#endif

		public void ApplyThemeTo(TTarget target, TId styleId) => GetStyle(styleId)?.ApplyThemeTo(target);
	}
}
