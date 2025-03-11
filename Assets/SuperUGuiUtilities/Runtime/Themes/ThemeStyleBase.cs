using System;
using UnityEngine;

namespace SuperUGuiUtilities {
	[Serializable]
	public abstract class ThemeStyleBase<TId>
	where TId : struct, Enum {
		[field: SerializeField, DisableIf(nameof(_preventEditing))]
		public TId StyleId { get; internal set; }
		[SerializeField, HideInInspector]
		internal bool _preventEditing = false;
	}

	public interface IProvidesThemeFor<T> where T : UnityEngine.Object {
		void ApplyThemeTo(T target);
	}
}
