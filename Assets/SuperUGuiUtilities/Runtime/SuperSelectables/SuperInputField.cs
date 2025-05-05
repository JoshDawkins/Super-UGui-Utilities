using System;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace SuperUGuiUtilities {
	[AddComponentMenu("UI/Super Selectables/Super Input Field")]
	public class SuperInputField : TMP_InputField, ISuperSelectable {
		public SelectableState CurrentState { get; private set; }

		public event Action<SelectableState, bool> OnStateChanged;


		protected override void DoStateTransition(SelectionState state, bool instant) {
			base.DoStateTransition(state, instant);

			CurrentState = state switch {
				SelectionState.Highlighted => SelectableState.Highlighted,
				SelectionState.Pressed => SelectableState.Pressed,
				SelectionState.Selected => SelectableState.Selected,
				SelectionState.Disabled => SelectableState.Disabled,
				_ => SelectableState.Normal
			};
			OnStateChanged?.Invoke(CurrentState, instant);
		}

#if UNITY_EDITOR
		[MenuItem("CONTEXT/TMP_InputField/Convert to Super Input Field")]
		private static void ConvertFromStandardButton(MenuCommand cmd) {
			var input = cmd.context as TMP_InputField;
			if (input == null || !EditorUtility.DisplayDialog("Convert to Super Input Field?",
				"Converting a standard TMP_InputField component to a SuperInputField will maintain all standard " +
				"TMP_InputField properties, but properties from any subclass of TMP_InputField and any serialized references " +
				"other components hold to this component will be lost. Proceed anyways?",
				"Yes, Convert", "No, Don't Convert"))
				return;

			var shouldActivateOnSelect = input.shouldActivateOnSelect;
			var shouldHideMobileInput = input.shouldHideMobileInput;
			var shouldHideSoftKeyboard = input.shouldHideSoftKeyboard;
			var text = input.text;
			var caretBlinkRate = input.caretBlinkRate;
			var caretWidth = input.caretWidth;
			var textViewport = input.textViewport;
			var textComponent = input.textComponent;
			var placeholder = input.placeholder;
			var verticalScrollbar = input.verticalScrollbar;
			var scrollSensitivity = input.scrollSensitivity;
			var caretColor = input.caretColor;
			var customCaretColor = input.customCaretColor;
			var selectionColor = input.selectionColor;
			var onEndEdit = input.onEndEdit;
			var onSubmit = input.onSubmit;
			var onSelect = input.onSelect;
			var onDeselect = input.onDeselect;
			var onTextSelection = input.onTextSelection;
			var onEndTextSelection = input.onEndTextSelection;
			var onValueChanged = input.onValueChanged;
			var onTouchScreenKeyboardStatusChanged = input.onTouchScreenKeyboardStatusChanged;
			var onValidateInput = input.onValidateInput;
			var characterLimit = input.characterLimit;
			var pointSize = input.pointSize;
			var fontAsset = input.fontAsset;
			var onFocusSelectAll = input.onFocusSelectAll;
			var resetOnDeActivation = input.resetOnDeActivation;
			var keepTextSelectionVisible = input.keepTextSelectionVisible;
			var restoreOriginalTextOnEscape = input.restoreOriginalTextOnEscape;
			var isRichTextEditingAllowed = input.isRichTextEditingAllowed;
			var contentType = input.contentType;
			var lineType = input.lineType;
			var lineLimit = input.lineLimit;
			var inputType = input.inputType;
			var keyboardType = input.keyboardType;
			var isAlert = input.isAlert;
			var characterValidation = input.characterValidation;
			var inputValidator = input.inputValidator;
			var readOnly = input.readOnly;
			var richText = input.richText;
			var asteriskChar = input.asteriskChar;

			SuperInputField super = ISuperSelectable.FromStandardSelectable<TMP_InputField, SuperInputField>(input);

			super.shouldActivateOnSelect = shouldActivateOnSelect;
			super.shouldHideMobileInput = input.shouldHideMobileInput;
			super.shouldHideSoftKeyboard = input.shouldHideSoftKeyboard;
			super.text = text;
			super.caretBlinkRate = caretBlinkRate;
			super.caretWidth = caretWidth;
			super.textViewport = textViewport;
			super.textComponent = textComponent;
			super.placeholder = placeholder;
			super.verticalScrollbar = verticalScrollbar;
			super.scrollSensitivity = scrollSensitivity;
			super.caretColor = caretColor;
			super.customCaretColor = customCaretColor;
			super.selectionColor = selectionColor;
			super.onEndEdit = onEndEdit;
			super.onSubmit = onSubmit;
			super.onSelect = onSelect;
			super.onDeselect = onDeselect;
			super.onTextSelection = onTextSelection;
			super.onEndTextSelection = onEndTextSelection;
			super.onValueChanged = onValueChanged;
			super.onTouchScreenKeyboardStatusChanged = onTouchScreenKeyboardStatusChanged;
			super.onValidateInput = onValidateInput;
			super.characterLimit = characterLimit;
			super.pointSize = pointSize;
			super.fontAsset = fontAsset;
			super.onFocusSelectAll = onFocusSelectAll;
			super.resetOnDeActivation = resetOnDeActivation;
			super.keepTextSelectionVisible = keepTextSelectionVisible;
			super.restoreOriginalTextOnEscape = restoreOriginalTextOnEscape;
			super.isRichTextEditingAllowed = isRichTextEditingAllowed;
			super.contentType = contentType;
			super.lineType = lineType;
			super.lineLimit = lineLimit;
			super.inputType = inputType;
			super.keyboardType = keyboardType;
			super.isAlert = isAlert;
			super.characterValidation = characterValidation;
			super.inputValidator = inputValidator;
			super.readOnly = readOnly;
			super.richText = richText;
			super.asteriskChar = asteriskChar;
		}
#endif
	}
}
