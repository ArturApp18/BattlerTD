using UnityEngine;

namespace Services.Input
{
	public abstract class InputService : IInputService
	{
		protected const string Vertical = "Vertical";
		protected const string Horizontal = "Horizontal";
		private const string Interaction = "Interaction";
		private const string AlternativeInteraction = "AlternativeInteraction";
		private const string Pause = "Pause";

		public abstract Vector2 Axis { get; }
		
		public bool IsActionButtonDown() =>
			SimpleInput.GetButtonDown(Interaction);

		public bool IsAlternativeActionButtonDown() =>
			SimpleInput.GetButtonDown(AlternativeInteraction);
		
		public bool IsPauseButtonDown() =>
			SimpleInput.GetButtonDown(Pause);

		protected static Vector2 SimpleInputAxis() =>
			new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
	}

}