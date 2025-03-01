using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace SuperUGuiUtilities.Editor {
	public abstract class ConditionalDrawer<T> : PropertyDrawer
	where T : ConditionalAttribute {
		private PropertyField field;
		private Label message;
		private T attr;
		private object target;

		public override VisualElement CreatePropertyGUI(SerializedProperty property) {
			VisualElement root = new();

			message = new Label();
			field = new PropertyField(property);
			attr = (T)attribute;
			target = property.GetTargetParent();

			message.SetErrorMessageStyles();
			message.ShowHide(false);

			field.schedule.Execute(CheckCondition).Every(500);

			root.Add(message);
			root.Add(field);
			return root;
		}

		private void CheckCondition() {
			HandleFieldDisplay(field, attr.IsConditionMet(target, out string error));

			message.text = error ?? string.Empty;
			message.ShowHide(!string.IsNullOrWhiteSpace(error));
		}
		protected abstract void HandleFieldDisplay(PropertyField field, bool conditionMet);
	}
}
