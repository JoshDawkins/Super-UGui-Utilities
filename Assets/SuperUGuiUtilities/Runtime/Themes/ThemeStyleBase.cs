using System;
using UnityEngine;

namespace SuperUGuiUtilities {
	[Serializable]
	public abstract class ThemeStyleBase<TId>
	where TId : struct, Enum {
		[field: SerializeField, DisableIf(nameof(_preventEditing))]
		public TId StyleId { get; protected internal set; }
		[SerializeField, HideInInspector]
		protected internal bool _preventEditing = false;
	}

	public interface IProvidesThemeFor<T> where T : Component {
		void ApplyThemeTo(T target);
	}
}
