using UnityEditor;
using UnityEditor.UIElements;

namespace SuperUGuiUtilities.Editor {
	[CustomPropertyDrawer(typeof(HideIfAttribute))]
	public class HideIfDrawer : ConditionalDrawer<HideIfAttribute> {
		protected override void HandleFieldDisplay(PropertyField field, bool conditionMet)
			=> field.ShowHide(!conditionMet);
	}
}
