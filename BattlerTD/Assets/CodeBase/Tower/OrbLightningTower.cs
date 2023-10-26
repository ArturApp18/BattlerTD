using UnityEngine;
using UnityEngine.VFX;

namespace CodeBase.Tower
{
	public class OrbLightningTower : PlaceableObject
	{
		private const string BaseColor = "Color";
		
		[SerializeField] private MeshRenderer _tower;
		[SerializeField] private VisualEffect _orb;

		private Color _originalColor;
		private void Awake()
		{
			_originalColor = _orb.GetVector4(BaseColor);
		}

		private void Update()
		{
			if (Placed)
				return;

			if (_buildingService.CanBePlaced(this, _inAnotherTower))
			{
				_tower.material.color = Color.green;

				_orb.SetVector4(BaseColor, new Vector4(0f, 1f, 0f, 1f));
			}
			else
			{
				_tower.material.color = Color.red;

				_orb.SetVector4(BaseColor, new Vector4(1f, 0f, 0f, 1f));
			}
		}

		public override void Place(ObjectDrag objectDrag)
		{
			base.Place(objectDrag);
			_orb.SetVector4(BaseColor, _originalColor);
		}
	}
}