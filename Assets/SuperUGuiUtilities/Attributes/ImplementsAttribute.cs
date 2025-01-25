using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
#if UNITY_EDITOR
using NaughtyAttributes.Editor;
#endif
using UnityEditor;

namespace SuperUGuiUtilities {
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class ImplementsAttribute : ValidatorAttribute {
		public Type[] ImplementingTypes { get; private set; }
		public bool ImplementsAll { get; set; } = false;


		public ImplementsAttribute(params Type[] implementingTypes) {
			ImplementingTypes = implementingTypes;
		}

		public string GetErrorMsg(string propName) {
			if ((ImplementingTypes?.Length ?? 0) < 1)
				return null;

			string andOr = ImplementsAll ? "and" : "or";
			string allAny = ImplementsAll ? "all" : "any";
			string list = ImplementingTypes.Length switch {
				1 => ImplementingTypes[0].Name,
				2 => $"{ImplementingTypes[0].Name} {andOr} {ImplementingTypes[1].Name}",
				_ => $"{allAny} of {string.Join(", ", ImplementingTypes.Take(ImplementingTypes.Length - 1).Select(t => t.Name))}, {andOr} {ImplementingTypes[^1].Name}"
			};
			return $"{propName} must implement {list}";
		}


#if UNITY_EDITOR
		static ImplementsAttribute() {
			var validators = typeof(ValidatorAttributeExtensions)
				.GetField("_validatorsByAttributeType", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
				.GetValue(null) as Dictionary<Type, PropertyValidatorBase>;

			validators.Add(typeof(ImplementsAttribute), new ImplementsPropertyValidator());
		}

		public class ImplementsPropertyValidator : PropertyValidatorBase {
			public override void ValidateProperty(SerializedProperty property) {
				ImplementsAttribute attr = PropertyUtility.GetAttribute<ImplementsAttribute>(property);

				if (property.propertyType != SerializedPropertyType.ObjectReference) {
					string warning = $"{attr.GetType().Name} works only on reference types";
					NaughtyEditorGUI.HelpBox_Layout(warning, MessageType.Warning, context: property.serializedObject.targetObject);
					return;
				}

				if (property.objectReferenceValue == null || (attr.ImplementingTypes?.Length ?? 0) < 1)
					return;

				Type type = property.objectReferenceValue.GetType();
				if (attr.ImplementsAll ? attr.ImplementingTypes.All(t => t.IsAssignableFrom(type)) : attr.ImplementingTypes.Any(t => t.IsAssignableFrom(type)))
					return;

				NaughtyEditorGUI.HelpBox_Layout(attr.GetErrorMsg(property.displayName), MessageType.Error, context: property.serializedObject.targetObject);
			}
		}
#endif
	}
}
