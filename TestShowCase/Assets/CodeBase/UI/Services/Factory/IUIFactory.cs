using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Skills;
using CodeBase.StaticData;
using CodeBase.UI.Windows.Shop;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public interface IUIFactory: IService
  {
    void CreateSkillPanel();
    SkillUI CreateSkillUI(SkillTypeId typeId, RectTransform skillUiPosition, Transform parent);
    void CreateShop();
    void CreateUIRoot();
  }
}