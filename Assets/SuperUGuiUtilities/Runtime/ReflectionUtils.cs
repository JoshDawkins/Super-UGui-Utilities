using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SuperUGuiUtilities {
	public static class ReflectionUtils {
		public static bool TryGetMemberValue<T>(object obj, string memberName, out T value, bool includeMethods = true) {
			value = default;
			if (obj == null)
				return false;

			System.Type objType = obj.GetType();
			while (objType != null) {
				FieldInfo field = objType
					.GetField(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (field != null && typeof(T).IsAssignableFrom(field.FieldType)) {
					value = (T)field.GetValue(obj);
					return true;
				}

				PropertyInfo prop = objType
					.GetProperty(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (prop != null && typeof(T).IsAssignableFrom(prop.PropertyType)) {
					value = (T)prop.GetValue(obj);
					return true;
				}

				if (includeMethods) {
					MethodInfo method = objType
						.GetMethod(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (method != null && typeof(T).IsAssignableFrom(method.ReturnType) && method.GetParameters().Length == 0) {
						value = (T)method.Invoke(obj, null);
						return true;
					}
				}

				objType = objType.BaseType;
			}

			return false;
		}
		public static bool TryGetMemberValue<T>(object obj, string memberName, int index, out T value, bool includeMethods = true) {
			value = default;
			if (!TryGetMemberValue(obj, memberName, out IEnumerable<T> enumerable, includeMethods) || enumerable == null)
				return false;

			if (enumerable.Count() <= index)
				return false;

			value = enumerable.ElementAt(index);
			return true;
		}

		public static MethodInfo GetMethodRecursive(this Type type, string methodName, BindingFlags bindingFlags, params Type[] argTypes) {
			if (argTypes == null)
				argTypes = Array.Empty<Type>();
			
			while (type != null) {
				MethodInfo method = type.GetMethod(methodName, bindingFlags, null, argTypes, null);
				if (method != null)
					return method;

				type = type.BaseType;
			}

			return null;
		}
		public static PropertyInfo GetPropertyRecursive(this Type type, string propName, BindingFlags bindingFlags) {
			while (type != null) {
				PropertyInfo prop = type.GetProperty(propName, bindingFlags);
				if (prop != null)
					return prop;

				type = type.BaseType;
			}

			return null;
		}
		public static FieldInfo GetFieldRecursive(this Type type, string fieldName, BindingFlags bindingFlags) {
			while (type != null) {
				FieldInfo field = type.GetField(fieldName, bindingFlags);
				if (field != null)
					return field;

				type = type.BaseType;
			}

			return null;
		}
	}
}
