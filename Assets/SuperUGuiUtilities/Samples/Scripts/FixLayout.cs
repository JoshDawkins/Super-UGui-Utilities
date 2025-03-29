using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities.Samples {
	public class FixLayout : MonoBehaviour {
		[SerializeField]
		private RectTransform rootRect;
		[SerializeField]
		private LayoutGroup layoutGroup;
		[SerializeField]
		private ContentSizeFitter sizeFitter;

		public void ForceResize() {
			if (sizeFitter != null) {
				sizeFitter.SetLayoutHorizontal();
				sizeFitter.SetLayoutVertical();
			}

			if (layoutGroup != null) {
				layoutGroup.SetLayoutHorizontal();
				layoutGroup.SetLayoutVertical();
			}

			LayoutRebuilder.ForceRebuildLayoutImmediate(rootRect);

			sizeFitter.TrySetEnabled(false);
			layoutGroup.TrySetEnabled(false);
			layoutGroup.TrySetEnabled(true);
			sizeFitter.TrySetEnabled(true);
		}
		public void ForceResizeNextFrame()
			=> this.InvokeNextFrame(ForceResize);

		private void Start() {
			ForceResizeNextFrame();
		}

#if UNITY_EDITOR
		private void OnValidate() {
			if (rootRect == null)
				rootRect = GetComponent<RectTransform>();
			if (layoutGroup == null)
				layoutGroup = GetComponent<LayoutGroup>();
			if (sizeFitter == null)
				sizeFitter = GetComponent<ContentSizeFitter>();
		}
#endif
	}
}
