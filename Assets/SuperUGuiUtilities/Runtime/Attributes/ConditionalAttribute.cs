using UnityEngine;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace SuperUGuiUtilities {
	public abstract class ConditionalAttribute : PropertyAttribute {
		private readonly IAttributeCondition condition;

		public ConditionalAttribute(string memberName)
			=> condition = new SingleCondition(memberName);
		public ConditionalAttribute(bool requiresAll, params string[] memberNames)
			=> condition = new MultiCondition(requiresAll, memberNames);
		public ConditionalAttribute(string enumMemberName, object enumMatchValue)
			=> condition = new EnumCondition(enumMemberName, enumMatchValue);

		public bool IsConditionMet(object obj, out string error)
			=> condition.IsConditionMet(obj, GetType().Name, out error);
		
		
		private interface IAttributeCondition {
			bool IsConditionMet(object obj, string callingType, out string error);
		}
		
		private class SingleCondition : IAttributeCondition {
			private readonly string memberName;

			public SingleCondition(string memberName) {
				this.memberName = memberName;
			}

			public bool IsConditionMet(object obj, string callingType, out string error) {
				if (obj == null) {
					error = "Object was null or destroyed";
					return false;
				}

				if (!ReflectionUtils.TryGetMemberValue(obj, memberName, out bool result)) {
					error = $"{callingType} could not find field, property, or zero-argument method '{memberName}' returning bool";
					return false;
				}

				error = null;
				return result;
			}
		}

		private class MultiCondition : IAttributeCondition {
			private readonly SingleCondition[] conditions;
			private readonly bool requiresAll;

            public MultiCondition(bool requiresAll, params string[] memberNames)
            {
				this.requiresAll = requiresAll;
				conditions = memberNames.Select(memberName => new SingleCondition(memberName)).ToArray();
            }

			public bool IsConditionMet(object obj, string callingType, out string error) {
				if (obj == null) {
					error = "Object was null or destroyed";
					return false;
				}

				StringBuilder errorBuilder = new();
				if (requiresAll) {
					if (conditions.All(cond => CheckConditionAndTrackError(cond))) {
						error = null;
						return true;
					}

					error = errorBuilder.Length > 0 ? errorBuilder.ToString() : null;
					return false;
				}

				bool result = conditions.Any(cond => CheckConditionAndTrackError(cond));
				error = errorBuilder.Length > 0 ? errorBuilder.ToString() : null;
				return result;


				bool CheckConditionAndTrackError(SingleCondition condition) {
					if (condition.IsConditionMet(obj, callingType, out string err))
						return true;

					if (!string.IsNullOrEmpty(err))
						errorBuilder.AppendLine(err);

					return false;
				}
			}
		}

		private class EnumCondition : IAttributeCondition {
			private readonly string memberName;
			private readonly object matchValue;

			public EnumCondition(string memberName, object matchValue) {
				this.memberName = memberName;
				this.matchValue = matchValue;
			}

			public bool IsConditionMet(object obj, string callingType, out string error) {
				System.Type matchType;
				if (matchValue == null || !(matchType = matchValue.GetType()).IsEnum) {
					error = $"Unsupported match value provided to {callingType}, must be an enum member";
					return false;
				}
				
				if (obj == null) {
					error = "Object was null or destroyed";
					return false;
				}

				MemberInfo[] members = obj.GetType().GetMember(memberName,
					MemberTypes.Field | MemberTypes.Property | MemberTypes.Method,
					BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

				if ((members?.Length ?? 0) < 1) {
					error = $"{callingType} could not find member '{memberName}'";
					return false;
				}

				error = null;
				IEqualityComparer comparer = (IEqualityComparer)typeof(EqualityComparer<>).MakeGenericType(matchType)
					.GetProperty("Default", BindingFlags.Public | BindingFlags.Static).GetValue(null);

				foreach (MemberInfo member in members) {
					switch (member) {
						case FieldInfo field:
							if (field.FieldType == matchType)
								return comparer.Equals(field.GetValue(obj), matchValue);
							break;

						case PropertyInfo prop:
							if (prop.PropertyType == matchType)
								return comparer.Equals(prop.GetValue(obj), matchValue);
							break;

						case MethodInfo method:
							if (method.ReturnType == matchType && (method.GetParameters()?.Length ?? 0) < 1)
								return comparer.Equals(method.Invoke(obj, null), matchValue);
							break;
					}
				}

				error = $"{callingType} requires a field, property, or zero-parameter method returning an enum of the same type as the match value";
				return false;
			}
		}
	}
}
