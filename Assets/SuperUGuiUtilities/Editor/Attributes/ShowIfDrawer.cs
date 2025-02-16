using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SuperUGuiUtilities.Editor {
	[CustomPropertyDrawer(typeof(ShowIfAttribute))]
	public class ShowIfDrawer : PropertyDrawer {
		private VisualElement field;
		private Label message;
		private ShowIfAttribute attr;
		private object target;
		
		public override VisualElement CreatePropertyGUI(SerializedProperty property) {
			VisualElement root = new();

			message = new Label();
			field = new PropertyField(property);
			attr = (ShowIfAttribute)attribute;
			target = property.GetTargetParent();

			message.style.whiteSpace = WhiteSpace.Normal;
			message.style.unityFontStyleAndWeight = FontStyle.Bold;
			message.style.paddingTop = 5;
			message.ShowHide(false);

			field.schedule.Execute(CheckShowCondition).Every(500);

			root.Add(message);
			root.Add(field);
			return root;
		}

		private void CheckShowCondition() {
			field.ShowHide(attr.IsConditionMet(target, out string error));

			message.text = error ?? string.Empty;
			message.ShowHide(!string.IsNullOrWhiteSpace(error));
		}
	}
}
