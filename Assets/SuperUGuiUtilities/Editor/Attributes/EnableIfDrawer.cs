using UnityEditor;
using UnityEditor.UIElements;

namespace SuperUGuiUtilities.Editor {
	[CustomPropertyDrawer(typeof(EnableIfAttribute))]
	public class EnableIfDrawer : ConditionalDrawer<EnableIfAttribute> {
		protected override void HandleFieldDisplay(PropertyField field, bool conditionMet)
			=> field.SetEnabled(conditionMet);
	}
}
