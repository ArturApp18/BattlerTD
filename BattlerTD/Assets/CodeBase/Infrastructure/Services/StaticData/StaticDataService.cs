using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.Tower;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string MonstersDataPath = "StaticData/Monsters";
    private const string LevelsDataPath = "StaticData/Levels";
    private const string HeroDataPath = "StaticData/Hero";
    private const string TowersDataPath = "StaticData/Towers";
    private const string StaticDataWindowPath = "StaticData/UI/WindowBase";

    private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
    private Dictionary<TowerType, TowerStaticData> _towers;
    private Dictionary<string, LevelStaticData> _levels;
    private Dictionary<WindowId, WindowConfig> _windowConfigs;
    private HeroStaticData _hero;

    public void Load()
    {
      _hero = Resources.Load<HeroStaticData>(HeroDataPath);

      _towers = Resources
        .LoadAll<TowerStaticData>(TowersDataPath)
        .ToDictionary(x => x.TowerTypeId, x => x);
      
      _monsters = Resources
        .LoadAll<MonsterStaticData>(MonstersDataPath)
        .ToDictionary(x => x.MonsterTypeId, x => x);

      _levels = Resources
        .LoadAll<LevelStaticData>(LevelsDataPath)
        .ToDictionary(x => x.LevelKey, x => x);

      _windowConfigs = Resources
        .Load<WindowStaticData>(StaticDataWindowPath)
        .Configs
        .ToDictionary(x => x.WindowId, x => x);
    }

    public MonsterStaticData ForMonster(MonsterTypeId typeId) =>
      _monsters.TryGetValue(typeId, out MonsterStaticData staticData)
        ? staticData
        : null;

    public LevelStaticData ForLevel(string sceneKey) =>
      _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
        ? staticData
        : null; 
    
    public TowerStaticData ForTower(TowerType towerType) =>
      _towers.TryGetValue(towerType, out TowerStaticData staticData)
        ? staticData
        : null;
    
    public WindowConfig ForWindow(WindowId window) =>
      _windowConfigs.TryGetValue(window, out WindowConfig windowConfig)
        ? windowConfig
        : null;

    public HeroStaticData ForHero() =>
      _hero;

  }
}