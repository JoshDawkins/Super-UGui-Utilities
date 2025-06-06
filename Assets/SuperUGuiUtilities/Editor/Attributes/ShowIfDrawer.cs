﻿using UnityEditor;
using UnityEditor.UIElements;

namespace SuperUGuiUtilities.Editor {
	[CustomPropertyDrawer(typeof(ShowIfAttribute))]
	public class ShowIfDrawer : ConditionalDrawer<ShowIfAttribute> {
		protected override void HandleFieldDisplay(PropertyField field, bool conditionMet)
			=> field.ShowHide(conditionMet);
	}
}
