using System;

namespace SuperUGuiUtilities {
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class EnableIfAttribute : ConditionalAttribute {
		public EnableIfAttribute(string memberName) : base(memberName) { }
		public EnableIfAttribute(bool requiresAll, params string[] memberNames)
			: base(requiresAll, memberNames) { }
		public EnableIfAttribute(string enumMemberName, object enumMatchValue)
			: base(enumMemberName, enumMatchValue) { }
	}
}
