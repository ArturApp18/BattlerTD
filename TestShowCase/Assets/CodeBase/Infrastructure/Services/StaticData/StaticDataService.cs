using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Services.Skills;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string MonstersDataPath = "StaticData/Monsters";
    private const string LevelsDataPath = "StaticData/Levels";
    private const string SkillsDataPath = "StaticData/Skills";
    private const string AllSkillsDataPath = "StaticData/Skills/AllSkills";
    private const string HeroDataPath = "StaticData/Hero";
    private const string StaticDataWindowPath = "StaticData/UI/WindowBase";

    private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
    private Dictionary<string, LevelStaticData> _levels;
    private Dictionary<WindowId, WindowConfig> _windowConfigs;
    private Dictionary<SkillTypeId, SkillStaticData> _skills;
    private HeroStaticData _hero;
    private AllSkillStaticData _allSkills;
    
    public void Load()
    {
      _hero = Resources.Load<HeroStaticData>(HeroDataPath);

      _allSkills = Resources.Load<AllSkillStaticData>(AllSkillsDataPath);
      
      _skills = Resources
        .LoadAll<SkillStaticData>(SkillsDataPath)
        .ToDictionary(x => x.SkillTypeId, x => x);
      
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

    public SkillStaticData ForSkill(SkillTypeId typeId) =>
        _skills.TryGetValue(typeId, out SkillStaticData staticData)
          ? staticData
          : null;

    public AllSkillStaticData ForAllSkills() =>
      _allSkills;

    public HeroStaticData ForHero() =>
      _hero;

    public WindowConfig ForWindow(WindowId window) =>
      _windowConfigs.TryGetValue(window, out WindowConfig windowConfig)
        ? windowConfig
        : null;

  }
}