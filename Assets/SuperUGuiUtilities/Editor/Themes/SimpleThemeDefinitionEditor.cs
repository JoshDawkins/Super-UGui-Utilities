using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace SuperUGuiUtilities.Editor {
	[CustomEditor(typeof(SimpleThemeDefinitionBase<,,>), true)]
	public class SimpleThemeDefinitionEditor : UnityEditor.Editor {
		public override VisualElement CreateInspectorGUI() {
			VisualElement root = new();

			root.Add(new PropertyField(serializedObject.FindBackingField("ThemeName")));

			ListView styleList = new();
			styleList.showAddRemoveFooter = false;
			styleList.showBoundCollectionSize = false;
			styleList.showFoldoutHeader = true;
			styleList.reorderable = false;
			styleList.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
			styleList.headerTitle = "Styles";
			styleList.BindProperty(serializedObject.FindProperty("styles"));
			root.Add(styleList);

			return root;
		}
	}
}
