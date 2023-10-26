using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Tower
{
	public class TestYo : MonoBehaviour
	{
		[SerializeField] private TriggerObserver _triggerObserver;
		
		private void Start()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;
		}

		private void OnDestroy()
		{
			_triggerObserver.TriggerEnter -= TriggerEnter;
			_triggerObserver.TriggerExit -= TriggerExit;
		}

		private void TriggerExit(Collider obj)
		{
			Debug.Log($"exit + {obj.name} + {obj.transform.position}");
		}

		private void TriggerEnter(Collider obj)
		{
			Debug.Log($"enter + {obj.name} + {obj.transform.position}");
		}
	}
}