using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    MonsterStaticData ForMonster(MonsterTypeId typeId);
    LevelStaticData ForLevel(string sceneKey);
    
    SkillStaticData ForSkill(SkillTypeId typeId);
    HeroStaticData ForHero();
    WindowConfig ForWindow(WindowId window);
    AllSkillStaticData ForAllSkills();
  }
}