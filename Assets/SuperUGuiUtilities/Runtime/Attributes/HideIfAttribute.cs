using System;

namespace SuperUGuiUtilities {
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class HideIfAttribute : ConditionalAttribute {
		public HideIfAttribute(string memberName) : base(memberName) { }
		public HideIfAttribute(bool requiresAll, params string[] memberNames)
			: base(requiresAll, memberNames) { }
		public HideIfAttribute(string enumMemberName, object enumMatchValue)
			: base(enumMemberName, enumMatchValue) { }
	}
}
