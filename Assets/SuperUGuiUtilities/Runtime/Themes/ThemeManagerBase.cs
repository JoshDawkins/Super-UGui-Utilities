using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SuperUGuiUtilities {
	public abstract class ThemeManagerBase<TTheme, TId, TStyle, TSelf> : ScriptableObject
	where TTheme : ThemeDefinitionBase<TId, TStyle>
	where TId : struct, Enum
	where TStyle : ThemeStyleBase<TId>
	where TSelf : ThemeManagerBase<TTheme, TId, TStyle, TSelf> {
		public static TSelf Instance {
			get {
				if (_instance == null) {
					_instance = Resources.FindObjectsOfTypeAll<TSelf>().ElementAtOrDefault(0);
					if (_instance != null && _instance.TryGetThemeAt(_instance.CurrentThemeIndex, out TTheme curr)) {
						curr.OnThemeUpdated -= _instance.FireThemeChanged;
						curr.OnThemeUpdated += _instance.FireThemeChanged;
					}
				}

				return _instance;
			}
		}

		private static TSelf _instance;


		[SerializeField]
		private List<TTheme> validThemes = new();
		[SerializeField, Min(0)]
		private int _currentThemeIndex = 0;

        public int CurrentThemeIndex {
			get => _currentThemeIndex;
			set {
				value = Mathf.Clamp(value, 0, validThemes.Count - 1);

				if (TryGetThemeAt(_currentThemeIndex, out TTheme curr))
					curr.OnThemeUpdated -= FireThemeChanged;

				_currentThemeIndex = value;
				FireThemeChanged();

				if (TryGetThemeAt(_currentThemeIndex, out curr))
					curr.OnThemeUpdated += FireThemeChanged;
			}
		}
		public TTheme CurrentTheme => this[CurrentThemeIndex];
		public TTheme this[int i] => TryGetThemeAt(i, out TTheme theme) ? theme : null;
		public IReadOnlyCollection<TTheme> ValidThemes => validThemes.AsReadOnly();

		public event Action OnThemeChanged;
		private void FireThemeChanged() => OnThemeChanged?.Invoke();


#if UNITY_EDITOR
		private void OnValidate() {
			CurrentThemeIndex = _currentThemeIndex;//Force any inspector-changed value through the prop for validation
		}
#endif

		public bool HasTheme(TTheme theme) => validThemes.Contains(theme);
		public int IndexOf(TTheme theme) => validThemes.IndexOf(theme);
		public bool TryGetThemeAt(int index, out TTheme theme) {
			if (index < 0 || index >= validThemes.Count) {
				theme = null;
				return false;
			}

			theme = validThemes[index];
			return true;
		}

		public bool TrySetCurrentTheme(TTheme theme) {
			int index = validThemes.IndexOf(theme);
			if (index < 0)
				return false;

			CurrentThemeIndex = index;
			return true;
		}
		public bool TryAddTheme(TTheme theme, out int index) {
			index = validThemes.IndexOf(theme);
			if (index > -1)
				return false;

			index = validThemes.Count;
			validThemes.Add(theme);
			return true;
		}
		public bool TryRemoveTheme(TTheme theme) {
			if (theme == null)
				return false;

			TTheme currTheme = CurrentTheme;
			bool removed = validThemes.Remove(theme);

			//Ensure that the currently selected theme doesn't change even if the removal
			//  changes its index, unless the current theme was the one removed, in which
			//  case make index 0 the current theme
			if (removed)
				CurrentThemeIndex = currTheme == theme ? 0 : IndexOf(currTheme);

			return removed;
		}

		public bool TryApplyStyleTo<T>(T target, TId styleId) where T : Component {
			TTheme theme = CurrentTheme;
			return theme != null && theme.TryApplyStyleTo(target, styleId);
		}
		public List<TId> GetValidIdsForTargetType<T>() where T : Component {
			TTheme theme = CurrentTheme;
			return theme == null ? null : theme.GetValidIdsForTargetType<T>();
		}
	}
}
