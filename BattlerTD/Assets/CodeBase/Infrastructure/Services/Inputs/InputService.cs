using UnityEngine;

namespace CodeBase.Infrastructure.Services.Inputs
{
	public abstract class InputService : IInputService
	{
		protected const string Horizontal = "Horizontal";
		protected const string Vertical = "Vertical";
		private const string Button = "Fire";
		private const string Button2 = "Fire2";
		private const string Button3 = "Fire3";
		private bool _isDragging;
		private int _towerButtonCounter;
		private Vector2 _startTouch, swipeDelta;

		public abstract Vector2 Axis { get; }
		public Touch Touch { get; set; }

		public bool Tap { get; set; }

		public bool SwipeLeft { get; set; }

		public bool SwipeRight { get; set; }

		public bool SwipeUp { get; set; } 

		public bool SwipeDown { get; set; }

		public bool SecondTowerTap { get; set; }
		public bool FirstTowerTap { get; set; } 

		public bool IsDragging
		{
			get
			{
				return _isDragging;
			}
		}

		public bool IsAttackButtonUp() =>
			SimpleInput.GetButtonUp(Button);

		public bool IsRadiusTowerButtonDown() =>
			SimpleInput.GetButtonDown(Button3);

		public bool IsRadiusTowerButtonUp() =>
			SimpleInput.GetButtonUp(Button3);

		public bool IsTurretTowerButtonDown() =>
			SimpleInput.GetButtonDown(Button2);

		public bool IsTurretTowerButtonUp() =>
			SimpleInput.GetButtonUp(Button2);


		protected static Vector2 SimpleInputAxis() =>
			new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));


		public void Update()
		{
			Tap = SwipeDown = SwipeUp = SwipeLeft = SwipeRight = false;

			if (Input.touches.Length > 0)
			{
				Touch = Input.GetTouch(0);
				if (Input.touches[0].phase == TouchPhase.Began)
				{
					Tap = true;
					_isDragging = true;
					_startTouch = Input.touches[0].position;
				}
				else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
				{
					_isDragging = false;
					Reset();
				}
			}

			CalculateDistance();

			if (CheckPassedDistance())
			{
				ChooseDirection();

				Reset();
			}
		}

		private void ChooseDirection()
		{
			float x = swipeDelta.x;
			float y = swipeDelta.y;
			if (Mathf.Abs(x) > Mathf.Abs(y))
			{
				if (x < 0)
					SwipeLeft = true;
				else
					SwipeRight = true;
			}
			else
			{
				if (y < 0)
					SwipeDown = true;
				else
					SwipeUp = true;
			}
		}

		public Touch GetTouch(Touch touch) =>
			touch;

		private bool CheckPassedDistance() =>
			swipeDelta.magnitude > 100;

		private void CalculateDistance()
		{
			swipeDelta = Vector2.zero;
			if (_isDragging)
			{
				if (Input.touches.Length < 0)
					swipeDelta = Input.touches[0].position - _startTouch;
				else if (Input.GetMouseButton(0))
					swipeDelta = (Vector2) Input.mousePosition - _startTouch;
			}
		}

		private void Reset()
		{
			_startTouch = swipeDelta = Vector2.zero;
			_isDragging = false;
		}
	}
}