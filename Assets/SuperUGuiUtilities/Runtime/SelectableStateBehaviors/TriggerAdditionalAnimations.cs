using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperUGuiUtilities {
	[ExecuteAlways]
	public class TriggerAdditionalAnimations : SelectableStateBehavior {
		[SerializeField]
		private AnimatorHandler[] targetAnimators;

		protected override void OnStateChanged(SelectableState state, bool instant)
			=> targetAnimators.ForEach(animator => animator.DoTransition(state));


		[Serializable]
		private class AnimatorHandler {
			[SerializeField]
			private Animator target;
			[SerializeField]
			private AnimationTriggers triggers = new();

			public void DoTransition(SelectableState state)
				=> triggers.ApplyStateTo(target, state);
		}
	}
}
