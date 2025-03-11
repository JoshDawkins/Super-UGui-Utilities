using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace SuperUGuiUtilities {
	public abstract class ThemeDefinitionBase<TId, TStyle> : ScriptableObject
	where TId : struct, Enum
	where TStyle : ThemeStyleBase<TId> {
		[field: SerializeField]
		public string ThemeName { get; private set; }

		public abstract ReadOnlyCollection<TStyle> AllStyles { get; }
		public abstract TStyle GetStyle(TId styleId);

		public bool TryApplyStyleTo<T>(T target, TId styleId) where T : UnityEngine.Object {
			TStyle style = GetStyle(styleId);
			if (style is IProvidesThemeFor<T> applicableTheme) {
				applicableTheme.ApplyThemeTo(target);
				return true;
			}
			return false;
		}

		public List<TId> GetValidIdsForTargetType<T>() where T : UnityEngine.Object
			=> AllStyles.Where(style => style is IProvidesThemeFor<T>).Select(style => style.StyleId).ToList();
	}
}
