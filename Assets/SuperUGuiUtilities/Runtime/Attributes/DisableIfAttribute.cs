using System;

namespace SuperUGuiUtilities {
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class DisableIfAttribute : ConditionalAttribute {
		public DisableIfAttribute(string memberName) : base(memberName) { }
		public DisableIfAttribute(bool requiresAll, params string[] memberNames)
			: base(requiresAll, memberNames) { }
		public DisableIfAttribute(string enumMemberName, object enumMatchValue)
			: base(enumMemberName, enumMatchValue) { }
	}
}
