using CodeBase.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
	public class UIFactory : IUIFactory
	{
		private const string UIRootPath = "UI/UIRoot";
		private readonly IAssetProvider _assets;
		private readonly IStaticDataService _staticData;

		private Transform _uiRoot;
		private readonly IPersistentProgressService _progressService;
		private readonly ITimerService _timerService;
		private TowerPanel _towerPanel;

		public Transform UiRoot
		{
			get
			{
				return _uiRoot;
			}
		}

		public UIFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService,
			ITimerService timerService)
		{
			_assets = assets;
			_staticData = staticData;
			_progressService = progressService;
			_timerService = timerService;
		}

		public TowerPanel TowerPanel
		{
			get
			{
				return _towerPanel;
			}
		}


		public void CreateUIRoot() =>
			_uiRoot = _assets.Instantiate(UIRootPath).transform;

		public void CreateShop()
		{
			
		}

	}

}