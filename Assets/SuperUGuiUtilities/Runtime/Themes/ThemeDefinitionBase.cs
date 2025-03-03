using System;
using UnityEngine;

namespace SuperUGuiUtilities {
	public abstract class ThemeDefinitionBase<TId, TStyle> : ScriptableObject
	where TId : struct, Enum
	where TStyle : ThemeStyleBase<TId> {
		[field: SerializeField]
		public string ThemeName { get; private set; }

		public abstract TStyle GetStyle(TId styleId);
	}

	[Serializable]
	public abstract class ThemeStyleBase<TId>
	where TId : struct, Enum {
		[field: SerializeField, DisableIf(nameof(_preventEditing))]
		public TId StyleId { get; private set; }
#pragma warning disable CS0414 //disable set but not used warning; used by DisableIf on StyleId via reflection
		//This value is set by reflection in SimpleThemeDefinitionBase; init-only prop would be better, but alas, Unity is too far behind
		[SerializeField, HideInInspector]
		private bool _preventEditing = false;
#pragma warning restore CS0414

		public override string ToString() => $"StyleId: {StyleId}";
	}

	public interface IProvidesThemeFor<T> where T : UnityEngine.Object {
		void ApplyThemeTo(T target);
	}
}
