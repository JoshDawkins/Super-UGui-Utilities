using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace SuperUGuiUtilities.Editor {
	public static class InspectorUtils {
		public const string USS_CLASS_HEADER = "unity-header-drawer__label";

		public static void ShowHide(this VisualElement elem, bool show)
			=> elem.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;

		public static void SetErrorMessageStyles(this Label label) {
			label.style.whiteSpace = WhiteSpace.Normal;
			label.style.unityFontStyleAndWeight = FontStyle.Bold;
			label.style.paddingTop = 5;
		}

		public static object GetTargetParent(this SerializedProperty prop) {
			object obj = prop.serializedObject.targetObject;
			string[] elements = prop.propertyPath.Replace(".Array.data[", "[").Split('.');

			//Loop through all but the last element
			for (int i = 0; i < elements.Length - 1; i++) {
				string element = elements[i];

				if (element.Contains("[")) {
					string name = element.Substring(0, element.IndexOf("["));
					int index = int.Parse(element.Substring(element.IndexOf("["))
						.Replace("[", "").Replace("]", ""));
					ReflectionUtils.TryGetMemberValue(obj, name, index, out obj, false);
				} else
					ReflectionUtils.TryGetMemberValue(obj, element, out obj, false);
			}

			return obj;
		}

		public static string GetBackingFieldName(string propName) => $"<{propName}>k__BackingField";
		public static SerializedProperty FindBackingField(this SerializedObject serializedObject, string propName)
			=> serializedObject?.FindProperty(GetBackingFieldName(propName));
		public static SerializedProperty FindBackingFieldRelative(this SerializedProperty serializedProperty, string propName)
			=> serializedProperty?.FindPropertyRelative(GetBackingFieldName(propName));
	}
}
