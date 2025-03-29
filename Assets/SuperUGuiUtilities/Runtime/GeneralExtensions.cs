using System;
using System.Collections;
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


		public static bool TrySetActive(this GameObject go, bool active) {
			if (go == null)
				return false;

			go.SetActive(active);
			return true;
		}
		public static bool TrySetActive(this Component component, bool active)
			=> component == null ? false : component.gameObject.TrySetActive(active);
		public static bool TrySetEnabled(this Behaviour behavior, bool active) {
			if (behavior == null)
				return false;

			behavior.enabled = active;
			return true;
		}


		public static bool TryGetDimension(this Rect rect, RectTransform.Axis axis, out float dimension) {
			if (rect == null) {
				dimension = 0;
				return false;
			}

			dimension = axis == RectTransform.Axis.Horizontal
				? rect.width : rect.height;
			return true;
		}
		public static bool TryGetDimension(this RectTransform rect, RectTransform.Axis axis, out float dimension)
			=> rect.rect.TryGetDimension(axis, out dimension);


		public static bool TrySetColor(this Graphic graphic, Color color) {
			if (graphic == null)
				return false;

			graphic.color = color;
			return true;
		}


		public static Coroutine TryStartCoroutine(this MonoBehaviour behavior, IEnumerator coroutine)
			=> behavior == null ? null : behavior.StartCoroutine(coroutine);
		public static Coroutine InvokeNextFrame(this MonoBehaviour behaviour, Action action)
			=> behaviour.TryStartCoroutine(WaitRoutine(action, null));
		public static Coroutine InvokeAtEndOfFrame(this MonoBehaviour behaviour, Action action)
			=> behaviour.TryStartCoroutine(WaitRoutine(action, new WaitForEndOfFrame()));
		public static Coroutine InvokeAfterSeconds(this MonoBehaviour behavior, Action action, float delayS)
			=> behavior.TryStartCoroutine(WaitRoutine(action, new WaitForSeconds(delayS)));
		public static Coroutine InvokeAfterSecondsRealtime(this MonoBehaviour behavior, Action action, float delayS)
			=> behavior.TryStartCoroutine(WaitRoutine(action, new WaitForSecondsRealtime(delayS)));
		public static Coroutine InvokeWhen(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition)
			=> behavior.TryStartCoroutine(WaitRoutine(action, new WaitUntil(triggerCondition)));
		public static Coroutine InvokeWhen(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition,
		float timeoutS, WaitTimeoutMode timeoutMode = WaitTimeoutMode.Realtime)
			=> behavior.InvokeWhen(action, triggerCondition, timeoutS, null, timeoutMode);
		public static Coroutine InvokeWhen(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition,
		float timeoutS, Action onTimeout, WaitTimeoutMode timeoutMode = WaitTimeoutMode.Realtime)
			=> behavior.InvokeWhen(action, triggerCondition, TimeSpan.FromSeconds(timeoutS), onTimeout, timeoutMode);
		public static Coroutine InvokeWhen(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition,
		TimeSpan timeout, WaitTimeoutMode timeoutMode = WaitTimeoutMode.Realtime)
			=> behavior.InvokeWhen(action, triggerCondition, timeout, null, timeoutMode);
		public static Coroutine InvokeWhen(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition,
		TimeSpan timeout, Action onTimeout, WaitTimeoutMode timeoutMode = WaitTimeoutMode.Realtime)
			=> behavior.TryStartCoroutine(WaitRoutine(action, new WaitUntil(triggerCondition, timeout, onTimeout, timeoutMode)));

		public static Coroutine InvokeEveryFrame(this MonoBehaviour behavior, Action action, bool includeThisFrame = true)
			=> behavior.InvokeEveryFrameWhile(action, () => true, includeThisFrame);
		public static Coroutine InvokeEveryFrameWhile(this MonoBehaviour behavior, Action action, Func<bool> whileCondition, bool includeThisFrame = true)
			=> behavior.TryStartCoroutine(LoopRoutine(action, null, whileCondition, includeThisFrame));
		public static Coroutine InvokeEverySeconds(this MonoBehaviour behavior, Action action, float intervalS, bool firstInvokeImmediately = true)
			=> behavior.InvokeEverySecondsWhile(action, intervalS, () => true, firstInvokeImmediately);
		public static Coroutine InvokeEverySecondsWhile(this MonoBehaviour behavior, Action action, float intervalS,
		Func<bool> whileCondition, bool firstInvokeImmediately = true)
			=> behavior.TryStartCoroutine(LoopRoutine(action, new WaitForSeconds(intervalS), whileCondition, firstInvokeImmediately));
		public static Coroutine InvokeEverySecondsRealtime(this MonoBehaviour behavior, Action action, float intervalS, bool firstInvokeImmediately = true)
			=> behavior.InvokeEverySecondsRealtimeWhile(action, intervalS, () => true, firstInvokeImmediately);
		public static Coroutine InvokeEverySecondsRealtimeWhile(this MonoBehaviour behavior, Action action, float intervalS,
		Func<bool> whileCondition, bool firstInvokeImmediately = true)
			=> behavior.TryStartCoroutine(LoopRoutine(action, new WaitForSecondsRealtime(intervalS), whileCondition, firstInvokeImmediately));
		public static Coroutine InvokeEveryWhen(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition, bool firstInvokeImmediately = true)
			=> behavior.InvokeEveryWhenWhile(action, triggerCondition, () => true, firstInvokeImmediately);
		public static Coroutine InvokeEveryWhen(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition, float timeoutS,
			bool firstInvokeImmediately = true, WaitTimeoutMode timeoutMode = WaitTimeoutMode.Realtime)
			=> behavior.InvokeEveryWhenWhile(action, triggerCondition, () => true, timeoutS, firstInvokeImmediately, timeoutMode);
		public static Coroutine InvokeEveryWhen(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition, float timeoutS, Action onTimeout,
			bool firstInvokeImmediately = true, WaitTimeoutMode timeoutMode = WaitTimeoutMode.Realtime)
			=> behavior.InvokeEveryWhenWhile(action, triggerCondition, () => true, timeoutS, onTimeout, firstInvokeImmediately, timeoutMode);
		public static Coroutine InvokeEveryWhen(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition, TimeSpan timeout,
			bool firstInvokeImmediately = true, WaitTimeoutMode timeoutMode = WaitTimeoutMode.Realtime)
			=> behavior.InvokeEveryWhenWhile(action, triggerCondition, () => true, timeout, firstInvokeImmediately, timeoutMode);
		public static Coroutine InvokeEveryWhen(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition, TimeSpan timeout, Action onTimeout,
			bool firstInvokeImmediately = true, WaitTimeoutMode timeoutMode = WaitTimeoutMode.Realtime)
			=> behavior.InvokeEveryWhenWhile(action, triggerCondition, () => true, timeout, onTimeout, firstInvokeImmediately, timeoutMode);
		public static Coroutine InvokeEveryWhenWhile(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition,
		Func<bool> whileCondition, bool firstInvokeImmediately = true)
			=> behavior.TryStartCoroutine(LoopRoutine(action, new WaitUntil(triggerCondition), whileCondition, firstInvokeImmediately));
		public static Coroutine InvokeEveryWhenWhile(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition,
		Func<bool> whileCondition, float timeoutS, bool firstInvokeImmediately = true, WaitTimeoutMode timeoutMode = WaitTimeoutMode.Realtime)
			=> behavior.InvokeEveryWhenWhile(action, triggerCondition, whileCondition, timeoutS, null, firstInvokeImmediately, timeoutMode);
		public static Coroutine InvokeEveryWhenWhile(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition,
		Func<bool> whileCondition, float timeoutS, Action onTimeout, bool firstInvokeImmediately = true, WaitTimeoutMode timeoutMode = WaitTimeoutMode.Realtime)
			=> behavior.InvokeEveryWhenWhile(action, triggerCondition, whileCondition, TimeSpan.FromSeconds(timeoutS), onTimeout, firstInvokeImmediately, timeoutMode);
		public static Coroutine InvokeEveryWhenWhile(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition,
		Func<bool> whileCondition, TimeSpan timeout, bool firstInvokeImmediately = true, WaitTimeoutMode timeoutMode = WaitTimeoutMode.Realtime)
			=> behavior.InvokeEveryWhenWhile(action, triggerCondition, whileCondition, timeout, null, firstInvokeImmediately, timeoutMode);
		public static Coroutine InvokeEveryWhenWhile(this MonoBehaviour behavior, Action action, Func<bool> triggerCondition,
		Func<bool> whileCondition, TimeSpan timeout, Action onTimeout, bool firstInvokeImmediately = true, WaitTimeoutMode timeoutMode = WaitTimeoutMode.Realtime)
			=> behavior.TryStartCoroutine(LoopRoutine(action, new WaitUntil(triggerCondition, timeout, onTimeout, timeoutMode), whileCondition, firstInvokeImmediately));

		private static IEnumerator WaitRoutine(Action action, object awaiter) {
			yield return awaiter;
			action?.Invoke();
		}
		private static IEnumerator LoopRoutine(Action action, object awaiter, Func<bool> whileCondition, bool firstInvokeImmediately) {
			bool firstInvokePassed = false;

			while (whileCondition?.Invoke() == true) {
				if (firstInvokePassed || firstInvokeImmediately)
					action?.Invoke();
				firstInvokePassed = true;

				yield return awaiter;
			}
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
		}
	}
}
