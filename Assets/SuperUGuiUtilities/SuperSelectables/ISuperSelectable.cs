using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	public interface ISuperSelectable {
		/// <summary>Returns the current state of the selectable</summary>
		SelectableState CurrentState { get; }

		/// <summary>Triggered when the selectable's state changes</summary>
		/// <remarks>Passes the new state, and a bool indicating whether any transition animations should be skipped (true) or allowed to play (false)</remarks>
		event Action<SelectableState, bool> OnStateChanged;

		/// <summary>Removes the given Selectable component and replaces it with the corresponding ISuperSelectable implementation</summary>
		/// <typeparam name="T">The type of Selectable to replace</typeparam>
		/// <typeparam name="TSuper">The ISuperSelectable implementation to replace with</typeparam>
		/// <param name="selectable">The Selectable component to replace</param>
		/// <returns>The new component of type <typeparamref name="TSuper"/> added to the same GameObject as <paramref name="selectable"/></returns>
		/// <remarks>
		///   <para>DestroyImmediate will be called on <paramref name="selectable"/>, meaning it will not be valid after calling this method.</para>
		///   <para>This method copies all <see cref="MonoBehaviour"/> and <see cref="Selectable"/> properties from <paramref name="selectable"/> to
		///     the returned component, but cannot handle properties specific to <typeparamref name="T"/>; implementing types can use this as a utility
		///     to simplify the process of this conversion by using the following structure: save type-specific properties locally, call ISuperSelectable
		///     .FromStandardSelectable(), apply saved properties to the returned value.</para>
		/// </remarks>
		protected static TSuper FromStandardSelectable<T, TSuper>(T selectable)
		where T : Selectable
		where TSuper : T, ISuperSelectable, new() {
			var go = selectable.gameObject;
			var navigation = selectable.navigation;
			var transition = selectable.transition;
			var colors = selectable.colors;
			var spriteState = selectable.spriteState;
			var animationTriggers = selectable.animationTriggers;
			var targetGraphic = selectable.targetGraphic;
			var interactable = selectable.interactable;
			var image = selectable.image;

			var enabled = selectable.enabled;
			var hideFlags = selectable.hideFlags;
			var name = selectable.name;
			var runInEditMode = selectable.runInEditMode;
			var tag = selectable.tag;
			var useGUILayout = selectable.useGUILayout;

			Component.DestroyImmediate(selectable);
			TSuper super = go.AddComponent<TSuper>();

			super.navigation = navigation;
			super.transition = transition;
			super.colors = colors;
			super.spriteState = spriteState;
			super.animationTriggers = animationTriggers;
			super.targetGraphic = targetGraphic;
			super.interactable = interactable;
			super.image = image;

			super.enabled = enabled;
			super.hideFlags = hideFlags;
			super.name = name;
			super.runInEditMode = runInEditMode;
			super.tag = tag;
			super.useGUILayout = useGUILayout;

			return super;
		}
	}
}
