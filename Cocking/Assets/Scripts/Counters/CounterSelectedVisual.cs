using Logic;
using UnityEngine;

namespace Counters
{
	public class CounterSelectedVisual : MonoBehaviour
	{
		[SerializeField] private bool _isSelected;
		[SerializeField] private GameObject[] SelectedVisual;
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private LayerMask layer;

		private void Awake()
		{
			_triggerObserver.TriggerEnter += TriggerEnter;
			_triggerObserver.TriggerExit += TriggerExit;
			_triggerObserver.TriggerStay += TriggerStay;
		}

		private void OnDestroy()
		{
			_triggerObserver.TriggerEnter -= TriggerEnter;
			_triggerObserver.TriggerExit -= TriggerExit;
			_triggerObserver.TriggerStay -= TriggerStay;
		}

		private void Update()
		{
			if (_isSelected)
			{
				foreach (var selected in SelectedVisual)
				{
					selected.SetActive(true);
				}
			}
			else
			{
				foreach (var selected in SelectedVisual)
				{
					selected.SetActive(false);
				}
			}
		}

		private void TriggerStay(Collider obj) { }

		private void TriggerExit(Collider obj)
		{
			if (obj.gameObject.layer == layer)
			{
				Debug.Log(obj.gameObject.name);
			}
			_isSelected = false;
			
		}

		private void TriggerEnter(Collider obj)
		{
			_isSelected = true;
			
		}
	}

}