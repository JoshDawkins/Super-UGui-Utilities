using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	public static class GeneralExtensions {
		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action) {
			if (collection == null || action == null)
				return;

			foreach (T item in collection)
				action.Invoke(item);
		}
		
		public static Color GetColorByState(this ColorBlock colors, SelectableState state)
			=> state switch {
				SelectableState.Normal => colors.normalColor,
				SelectableState.Highlighted => colors.highlightedColor,
				SelectableState.Pressed => colors.pressedColor,
				SelectableState.Selected => colors.selectedColor,
				SelectableState.Disabled => colors.disabledColor,
				_ => Color.black
			};
		public static void ApplyStateTo(this ColorBlock colors, Graphic target, SelectableState state, bool instant) {
			if (target == null)
				return;

			target.CrossFadeColor(colors.GetColorByState(state) * colors.colorMultiplier,
				instant ? 0f : colors.fadeDuration, true, true);
		}

		public static Sprite GetSpriteByState(this SpriteState sprites, SelectableState state)
			=> state switch {
				SelectableState.Highlighted => sprites.highlightedSprite,
				SelectableState.Pressed => sprites.pressedSprite,
				SelectableState.Selected => sprites.selectedSprite,
				SelectableState.Disabled => sprites.disabledSprite,
				_ => null
			};
		public static void ApplyStateTo(this SpriteState sprites, Image target, SelectableState state) {
			if (target != null)
				target.overrideSprite = sprites.GetSpriteByState(state);
		}

		public static string GetTriggerNameByState(this AnimationTriggers triggers, SelectableState state)
			=> state switch {
				SelectableState.Normal => triggers?.normalTrigger,
				SelectableState.Highlighted => triggers?.highlightedTrigger,
				SelectableState.Pressed => triggers?.pressedTrigger,
				SelectableState.Selected => triggers?.selectedTrigger,
				SelectableState.Disabled => triggers?.disabledTrigger,
				_ => string.Empty
			};
		public static void ApplyStateTo(this AnimationTriggers triggers, Animator target, SelectableState state) {
#if PACKAGE_ANIMATION
            if (triggers == null || target == null || !target.isActiveAndEnabled || !target.hasBoundPlayables)
                return;

			string trigger = triggers.GetTriggerNameByState(state);
			if (string.IsNullOrEmpty(trigger))
				return;

            target.ResetTrigger(triggers.normalTrigger);
            target.ResetTrigger(triggers.highlightedTrigger);
            target.ResetTrigger(triggers.pressedTrigger);
            target.ResetTrigger(triggers.selectedTrigger);
            target.ResetTrigger(triggers.disabledTrigger);

            target.SetTrigger(trigger);
#endif
		}
	}
}
