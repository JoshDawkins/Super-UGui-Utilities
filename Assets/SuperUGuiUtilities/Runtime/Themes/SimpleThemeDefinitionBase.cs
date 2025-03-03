using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using BindingFlags = System.Reflection.BindingFlags;

namespace SuperUGuiUtilities {
	public abstract class SimpleThemeDefinitionBase<TId, TStyle, TTarget> : ThemeDefinitionBase<TId, TStyle>
	where TId : struct, Enum
	where TStyle : ThemeStyleBase<TId>, IProvidesThemeFor<TTarget>
	where TTarget : UnityEngine.Object {
		[SerializeField]
		protected List<TStyle> styles;

#if UNITY_EDITOR
		//This ensures that we always have a style for each ID, even when the enum definition is changed.
		//  Only applied in editor, since enum definitions can't change at runtime and values are serialized.
		private void OnEnable() {
			Type styleType = typeof(TStyle);
			PropertyInfo styleIdProp = styleType.GetProperty("StyleId").DeclaringType.GetProperty("StyleId");
			FieldInfo preventEditingField = styleType.GetFieldRecursive("_preventEditing",
				BindingFlags.Instance | BindingFlags.NonPublic);
			TId[] ids = (TId[])Enum.GetValues(typeof(TId));

			styles.RemoveAll(style => !ids.Contains(style.StyleId));

			foreach (TId id in ids) {
				if (styles.Find(style => style.StyleId.Equals(id)) != null)
					continue;

				TStyle style = (TStyle)Activator.CreateInstance(styleType);
				styleIdProp.SetValue(style, id, BindingFlags.Instance | BindingFlags.NonPublic, null, null, null);
				preventEditingField.SetValue(style, true);
				styles.Add(style);
			}

			styles = styles.OrderBy(style => style.StyleId).ToList();
		}
#endif

		public override TStyle GetStyle(TId styleId) => styles.FirstOrDefault(s => s.StyleId.Equals(styleId));
		public void ApplyThemeTo(TTarget target, TId styleId) => GetStyle(styleId)?.ApplyThemeTo(target);
	}
}
