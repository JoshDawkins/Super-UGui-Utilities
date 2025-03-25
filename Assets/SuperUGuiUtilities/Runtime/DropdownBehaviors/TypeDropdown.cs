using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace SuperUGuiUtilities {
	public abstract class TypeDropdown<T> : DropdownBehavior {
		[SerializeField]
		protected List<T> options = new();
		[SerializeField]
		private UnityEvent<T> onOptionChosen = new();

		public event UnityAction<T> OnOptionChosen {
			add => onOptionChosen.AddListener(value);
			remove => onOptionChosen.RemoveListener(value);
		}

		private bool isInitialized = false;


		protected override void Start() {
			targetDropdown.ClearOptions();
			targetDropdown.AddOptions(options.Select(o => GetOptionTitle(o)).ToList());
			isInitialized = true;
		}
		protected virtual string GetOptionTitle(T option) => option.ToString();

		protected sealed override void OnOptionSelected(int index) => onOptionChosen?.Invoke(options[index]);

		public bool AddOption(T option) {
			if (options.Contains(option))
				return false;

			options.Add(option);
			if (isInitialized)
				targetDropdown.AddOptions(new List<string>() { GetOptionTitle(option) });

			return true;
		}
		public bool RemoveOption(T option) {
			int index = options.IndexOf(option);
			if (index < 0)
				return false;

			options.RemoveAt(index);
			if (isInitialized)
				targetDropdown.options.RemoveAt(index);

			return true;
		}
	}
}
