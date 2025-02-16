using System;

namespace SuperUGuiUtilities {
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class ShowIfAttribute : ConditionalAttribute {
		public ShowIfAttribute(string memberName) : base(memberName) { }
		public ShowIfAttribute(bool requiresAll, params string[] memberNames)
			: base(requiresAll, memberNames) { }
		public ShowIfAttribute(string enumMemberName, object enumMatchValue)
			: base(enumMemberName, enumMatchValue) { }
	}
}
