using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace SuperUGuiUtilities.Editor {
	public abstract class ConditionalDrawer<T> : PropertyDrawer
	where T : ConditionalAttribute {
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
			=> new ConditionalField(property, (T)attribute, HandleFieldDisplay);

		protected abstract void HandleFieldDisplay(PropertyField field, bool conditionMet);
	}

	public class ConditionalField : VisualElement {
		private readonly PropertyField field;
		private readonly Label message;
		private readonly ConditionalAttribute attr;
		private readonly object target;
		private readonly Action<PropertyField, bool> fieldDislayCallback;

		public ConditionalField(SerializedProperty property, ConditionalAttribute attribute, Action<PropertyField, bool> fieldDislayCallback)
        {
			message = new Label();
			field = new PropertyField(property);
			attr = attribute;
			target = property.GetTargetParent();
			this.fieldDislayCallback = fieldDislayCallback;

			message.SetErrorMessageStyles();
			message.ShowHide(false);

			field.schedule.Execute(CheckCondition).Every(500);

			Add(message);
			Add(field);
		}

		private void CheckCondition() {
			fieldDislayCallback(field, attr.IsConditionMet(target, out string error));

			message.text = error ?? string.Empty;
			message.ShowHide(!string.IsNullOrWhiteSpace(error));
		}
	}
}
