using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	[AddComponentMenu("UI/Super Selectables/Super Slider")]
	public class SuperSlider : Slider, ISuperSelectable {
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
		[MenuItem("CONTEXT/Slider/Convert to Super Slider")]
		private static void ConvertFromStandardSlider(MenuCommand cmd) {
			var slide = cmd.context as Slider;
			if (slide == null || !EditorUtility.DisplayDialog("Convert to Super Slider?",
				"Converting a standard Slider component to a SuperSlider will maintain all standard " +
				"Slider properties, but properties from any subclass of Slider and any serialized references " +
				"other components hold to this component will be lost. Proceed anyways?",
				"Yes, Convert", "No, Don't Convert"))
				return;

			var fillRect = slide.fillRect;
			var handleRect = slide.handleRect;
			var direction = slide.direction;
			var minValue = slide.minValue;
			var maxValue = slide.maxValue;
			var wholeNumbers = slide.wholeNumbers;
			var value = slide.value;
			var onValueChanged = slide.onValueChanged;

			SuperSlider super = ISuperSelectable.FromStandardSelectable<Slider, SuperSlider>(slide);

			super.fillRect = fillRect;
			super.handleRect = handleRect;
			super.direction = direction;
			super.minValue = minValue;
			super.maxValue = maxValue;
			super.wholeNumbers = wholeNumbers;
			super.value = value;
			super.onValueChanged = onValueChanged;
		}
#endif
	}
}
