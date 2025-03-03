using UnityEditor;
using UnityEditor.UIElements;

namespace SuperUGuiUtilities.Editor {
	[CustomPropertyDrawer(typeof(DisableIfAttribute))]
	public class DisableIfDrawer : ConditionalDrawer<DisableIfAttribute> {
		protected override void HandleFieldDisplay(PropertyField field, bool conditionMet)
			=> field.SetEnabled(!conditionMet);
	}
}
