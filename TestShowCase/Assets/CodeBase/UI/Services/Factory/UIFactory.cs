using System;
using CodeBase.AssetManagement;
using CodeBase.Infrastructure.Services.Ads;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Skills;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Shop;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Services.Factory
{
  public class UIFactory : IUIFactory
  {
    private const string UIRootPath = "UI/UIRoot";
    private readonly IAssetProvider _assets;
    private readonly IStaticDataService _staticData;
    
    private Transform _uiRoot;
    private readonly IPersistentProgressService _progressService;
    private readonly IAdsService _adsService;
    private readonly ISkillService _skillService;
    private readonly ITimerService _timerService;

    public UIFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService,
      IAdsService adsService, ISkillService skillService, ITimerService timerService)
    {
      _assets = assets;
      _staticData = staticData;
      _progressService = progressService;
      _adsService = adsService;
      _skillService = skillService;
      _timerService = timerService;
    }

    public SkillUI CreateSkillUI(SkillTypeId skillTypeId, RectTransform skillUiPosition, Transform parent)
    {
      SkillStaticData skillUIConfig = _staticData.ForSkill(skillTypeId);
      SkillUI skillUi = Object.Instantiate(skillUIConfig.SkillUIPrefab, skillUiPosition.position, Quaternion.identity, parent).GetComponent<SkillUI>();
      skillUi.Description.text = skillUIConfig.Description;
      skillUi.Level.text = skillUIConfig.Level;
      skillUi.Name.text = skillUIConfig.Name;
      skillUi.SkillImage.sprite = skillUIConfig.SkillImage;
      skillUi.SkillTypeId = skillUIConfig.SkillTypeId;
      return skillUi;
    }

    public void CreateShop()
    {
      WindowConfig config = _staticData.ForWindow(WindowId.Shop);
      ShopWindow window = Object.Instantiate(config.Template, _uiRoot) as ShopWindow;
      window.Construct(_adsService,_progressService, _timerService);
    }
    
    public void CreateSkillPanel()
    {
      WindowConfig config = _staticData.ForWindow(WindowId.SkillPanel);
      SkillPanel window = Object.Instantiate(config.Template, _uiRoot) as SkillPanel;
      window.Construct(_skillService, _progressService, _timerService);
    }

    public void CreateUIRoot() => 
      _uiRoot = _assets.Instantiate(UIRootPath).transform;
  }
}