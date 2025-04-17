using System;
using System.Reflection;
using UnityEngine;

namespace SuperUGuiUtilities {
	[ExecuteAlways]
	public abstract class ThemedElementBase<TTarget, TManager, TTheme, TId, TStyle> : MonoBehaviour
	where TTarget : Component
	where TManager : ThemeManagerBase<TTheme, TId, TStyle, TManager>
	where TTheme : ThemeDefinitionBase<TId, TStyle>
	where TId : struct, Enum
	where TStyle : ThemeStyleBase<TId> {
		[SerializeField]
		private TTarget target;
		[SerializeField]
		private TId _style;
		public TId Style {
			get => _style;
			set {
				if (value.Equals(_style) || !Enum.IsDefined(typeof(TId), value))
					return;

				_style = value;
				UpdateTheme();
			}
		}

		protected TManager manager;


		private void OnEnable() {
			manager = typeof(TManager).GetPropertyRecursive("Instance",
				BindingFlags.Static | BindingFlags.Public)?.GetValue(null) as TManager;
			if (manager == null) {
				Debug.LogError($"{GetType().Name} could not find a manager of type {typeof(TManager).Name}", this);
				enabled = false;
				return;
			}

			manager.OnThemeChanged += UpdateTheme;
		}
		private void OnDisable() {
			if (manager != null)
				manager.OnThemeChanged -= UpdateTheme;
		}
#if UNITY_EDITOR
		private void OnValidate() {
			if (target == null)
				target = GetComponent<TTarget>();

			UpdateTheme();
		}
#endif

		private void UpdateTheme() {
			if (isActiveAndEnabled && this != null)
				this.InvokeAtEndOfFrame(UpdateThemeInternal);//Ensures we're not calling built-in methods at an inappropriate time
		}

		private void UpdateThemeInternal() {
			if (!enabled || target == null || manager == null)
				return;

			if (!manager.TryApplyStyleTo(target, Style))
				Debug.LogWarning($"Cannot set style {Style} to target of type {target.GetType().Name}", this);
		}
	}
}
