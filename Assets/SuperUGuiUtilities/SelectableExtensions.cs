using System;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	public static class SelectableExtensions {
		/// <summary>Attempts to get the value of <see cref="Selectable.IsInteractable"/> for this selectable, if it is not null</summary>
		/// <param name="selectable">The selectable to check</param>
		/// <param name="isInteractable">If true is returned, will contain the result of calling <see cref="Selectable.IsInteractable"/> on the selectable</param>
		/// <returns>true if the selectable is not null, false otherwise</returns>
		public static bool TryGetIsInteractable(this Selectable selectable, out bool isInteractable) {
			if (selectable == null)
				return isInteractable = false;

			isInteractable = selectable.IsInteractable();
			return true;
		}
		/// <summary>Attempts to set <see cref="Selectable.interactable"/> on this selectable, if it is not null</summary>
		/// <param name="selectable">The selectable to modify</param>
		/// <param name="interactable">Whether the selectable should be interactable or not</param>
		public static void TrySetInteractable(this Selectable selectable, bool interactable) {
			if (selectable != null)
				selectable.interactable = interactable;
		}

		/// <summary>Try to add the given <paramref name="callback"/> to this selectable's <see cref="ISuperSelectable.OnStateChanged"/> event,
		/// if the selectable is not null</summary>
		/// <param name="super">The Super Selectable to modify</param>
		/// <param name="callback">The callback to add</param>
		/// <returns>True if the selectable is not null, false otherwise</returns>
		public static bool TryAddStateChangedListener(this ISuperSelectable super, Action<SelectableState, bool> callback) {
			if (super == null)
				return false;

			super.OnStateChanged += callback;
			return true;
		}
		/// <summary>Try to remove the given <paramref name="callback"/> from this selectable's <see cref="ISuperSelectable.OnStateChanged"/> event,
		/// if the selectable is not null</summary>
		/// <param name="super">The Super Selectable to modify</param>
		/// <param name="callback">The callback to remove</param>
		/// <returns>True if the selectable is not null, false otherwise</returns>
		public static bool TryRemoveStateChangedListener(this ISuperSelectable super, Action<SelectableState, bool> callback) {
			if (super == null)
				return false;

			super.OnStateChanged -= callback;
			return true;
		}

		/// <summary>Try to add the given <paramref name="callback"/> to this button's <see cref="Button.onClick"/> event, if the button is not null</summary>
		/// <param name="button">The button to modify</param>
		/// <param name="callback">The callback to add</param>
		public static void TryAddClickListener(this Button button, UnityAction callback) {
			if (button != null)
				button.onClick.AddListener(callback);
		}
		/// <summary>Try to remove the given <paramref name="callback"/> from this button's <see cref="Button.onClick"/> event, if the button is not null</summary>
		/// <param name="button">The button to modify</param>
		/// <param name="callback">The callback to remove</param>
		public static void TryRemoveClickListener(this Button button, UnityAction callback) {
			if (button != null)
				button.onClick.RemoveListener(callback);
		}
		/// <summary>Try to remove all non-persistent (created from script) listeners from this button's <see cref="Button.onClick"/> event, if the button is not null</summary>
		/// <param name="button">The button to modify</param>
		public static void TryRemoveAllClickListeners(this Button button) {
			if (button != null)
				button.onClick.RemoveAllListeners();
		}

		/// <summary>Try to add the given <paramref name="callback"/> to this dropdown's <see cref="TMP_Dropdown.onValueChanged"/> event, if the dropdown is not null</summary>
		/// <param name="dropdown">The dropdown to modify</param>
		/// <param name="callback">The callback to add</param>
		public static void TryAddValueChangedListener(this TMP_Dropdown dropdown, UnityAction<int> callback) {
			if (dropdown != null)
				dropdown.onValueChanged.AddListener(callback);
		}
		/// <summary>Try to remove the given <paramref name="callback"/> from this dropdown's <see cref="TMP_Dropdown.onValueChanged"/> event, if the dropdown is not null</summary>
		/// <param name="dropdown">The dropdown to modify</param>
		/// <param name="callback">The callback to remove</param>
		public static void TryRemoveValueChangedListener(this TMP_Dropdown dropdown, UnityAction<int> callback) {
			if (dropdown != null)
				dropdown.onValueChanged.RemoveListener(callback);
		}
		/// <summary>Try to remove all non-persistent (created from script) listeners from this dropdown's <see cref="TMP_Dropdown.onValueChanged"/> event,
		/// if the dropdown is not null</summary>
		/// <param name="dropdown">The dropdown to modify</param>
		public static void TryRemoveAllValueChangedListeners(this TMP_Dropdown dropdown) {
			if (dropdown != null)
				dropdown.onValueChanged.RemoveAllListeners();
		}

		/// <summary>Try to add the given <paramref name="callback"/> to this input's <see cref="TMP_InputField.onValueChanged"/> event, if the input is not null</summary>
		/// <param name="input">The input field to modify</param>
		/// <param name="callback">The callback to add</param>
		public static void TryAddOnValueChangedListener(this TMP_InputField input, UnityAction<string> callback) {
			if (input != null)
				input.onValueChanged.AddListener(callback);
		}
		/// <summary>Try to remove the given <paramref name="callback"/> from this input's <see cref="TMP_InputField.onValueChanged"/> event, if the input is not null</summary>
		/// <param name="input">The input field to modify</param>
		/// <param name="callback">The callback to remove</param>
		public static void TryRemoveOnValueChangedListener(this TMP_InputField input, UnityAction<string> callback) {
			if (input != null)
				input.onValueChanged.RemoveListener(callback);
		}
		/// <summary>Try to remove all non-persistent (created from script) listeners from this input's <see cref="TMP_Dropdown.onValueChanged"/> event,
		/// if the input is not null</summary>
		/// <param name="input">The input field to modify</param>
		public static void TryRemoveAllOnValueChangedListeners(this TMP_InputField input) {
			if (input != null)
				input.onValueChanged.RemoveAllListeners();
		}

		/// <summary>Try to add the given <paramref name="callback"/> to this input's <see cref="TMP_InputField.onSubmit"/> event, if the input is not null</summary>
		/// <param name="input">The input field to modify</param>
		/// <param name="callback">The callback to add</param>
		public static void TryAddOnSubmitListener(this TMP_InputField input, UnityAction<string> callback) {
			if (input != null)
				input.onSubmit.AddListener(callback);
		}
		/// <summary>Try to remove the given <paramref name="callback"/> from this input's <see cref="TMP_InputField.onSubmit"/> event, if the input is not null</summary>
		/// <param name="input">The input field to modify</param>
		/// <param name="callback">The callback to remove</param>
		public static void TryRemoveOnSubmitListener(this TMP_InputField input, UnityAction<string> callback) {
			if (input != null)
				input.onSubmit.RemoveListener(callback);
		}
		/// <summary>Try to remove all non-persistent (created from script) listeners from this input's <see cref="TMP_Dropdown.onSubmit"/> event,
		/// if the input is not null</summary>
		/// <param name="input">The input field to modify</param>
		public static void TryRemoveAllOnSubmitListeners(this TMP_InputField input) {
			if (input != null)
				input.onSubmit.RemoveAllListeners();
		}

		/// <summary>Try to add the given <paramref name="callback"/> to this scrollbar's <see cref="Scrollbar.onValueChanged"/> event, if the scrollbar is not null</summary>
		/// <param name="scroll">The scrollbar to modify</param>
		/// <param name="callback">The callback to add</param>
		public static void TryAddOnValueChangedListener(this Scrollbar scroll, UnityAction<float> callback) {
			if (scroll != null)
				scroll.onValueChanged.AddListener(callback);
		}
		/// <summary>Try to remove the given <paramref name="callback"/> from this scrollbar's <see cref="Scrollbar.onValueChanged"/> event, if the scrollbar is not null</summary>
		/// <param name="scroll">The scrollbar to modify</param>
		/// <param name="callback">The callback to remove</param>
		public static void TryRemoveOnValueChangedListener(this Scrollbar scroll, UnityAction<float> callback) {
			if (scroll != null)
				scroll.onValueChanged.RemoveListener(callback);
		}
		/// <summary>Try to remove all non-persistent (created from script) listeners from this scrollbar's <see cref="Scrollbar.onValueChanged"/> event,
		/// if the scrollbar is not null</summary>
		/// <param name="scroll">The scrollbar to modify</param>
		public static void TryRemoveAllOnValueChangedListeners(this Scrollbar scroll) {
			if (scroll != null)
				scroll.onValueChanged.RemoveAllListeners();
		}

		/// <summary>Try to add the given <paramref name="callback"/> to this slider's <see cref="Slider.onValueChanged"/> event, if the slider is not null</summary>
		/// <param name="slider">The slider to modify</param>
		/// <param name="callback">The callback to add</param>
		public static void TryAddOnValueChangedListener(this Slider slider, UnityAction<float> callback) {
			if (slider != null)
				slider.onValueChanged.AddListener(callback);
		}
		/// <summary>Try to remove the given <paramref name="callback"/> from this slider's <see cref="Slider.onValueChanged"/> event, if the slider is not null</summary>
		/// <param name="slider">The slider to modify</param>
		/// <param name="callback">The callback to remove</param>
		public static void TryRemoveOnValueChangedListener(this Slider slider, UnityAction<float> callback) {
			if (slider != null)
				slider.onValueChanged.RemoveListener(callback);
		}
		/// <summary>Try to remove all non-persistent (created from script) listeners from this slider's <see cref="Slider.onValueChanged"/> event,
		/// if the slider is not null</summary>
		/// <param name="slider">The slider to modify</param>
		public static void TryRemoveAllOnValueChangedListeners(this Slider slider) {
			if (slider != null)
				slider.onValueChanged.RemoveAllListeners();
		}

		/// <summary>Try to add the given <paramref name="callback"/> to this toggle's <see cref="Toggle.onValueChanged"/> event, if the toggle is not null</summary>
		/// <param name="toggle">The toggle to modify</param>
		/// <param name="callback">The callback to add</param>
		public static void TryAddOnValueChangedListener(this Toggle toggle, UnityAction<bool> callback) {
			if (toggle != null)
				toggle.onValueChanged.AddListener(callback);
		}
		/// <summary>Try to remove the given <paramref name="callback"/> from this toggle's <see cref="Toggle.onValueChanged"/> event, if the toggle is not null</summary>
		/// <param name="toggle">The toggle to modify</param>
		/// <param name="callback">The callback to remove</param>
		public static void TryRemoveOnValueChangedListener(this Toggle toggle, UnityAction<bool> callback) {
			if (toggle != null)
				toggle.onValueChanged.RemoveListener(callback);
		}
		/// <summary>Try to remove all non-persistent (created from script) listeners from this toggle's <see cref="Toggle.onValueChanged"/> event,
		/// if the toggle is not null</summary>
		/// <param name="toggle">The toggle to modify</param>
		public static void TryRemoveAllOnValueChangedListeners(this Toggle toggle) {
			if (toggle != null)
				toggle.onValueChanged.RemoveAllListeners();
		}

		/// <summary>Try to set the state of this toggle, if it is not null</summary>
		/// <param name="toggle">The toggle to modify</param>
		/// <param name="isOn">Whether or not the toggle should be on</param>
		/// <param name="withoutNotify">If true, the toggle's <see cref="Toggle.onValueChanged"/> event will not be fired</param>
		public static void TrySetIsOn(this Toggle toggle, bool isOn, bool withoutNotify = false) {
			if (toggle == null)
				return;

			if (withoutNotify)
				toggle.SetIsOnWithoutNotify(isOn);
			else
				toggle.isOn = isOn;
		}
		/// <summary>Try to set the <see cref="ToggleGroup"/> this toggle is associated with, if the toggle is not null</summary>
		/// <param name="toggle">The toggle to modify</param>
		/// <param name="group">The <see cref="ToggleGroup"/> to associate the toggle with (can be null)</param>
		public static void TrySetGroup(this Toggle toggle, ToggleGroup group) {
			if (toggle != null)
				toggle.group = group;
		}
	}
}
