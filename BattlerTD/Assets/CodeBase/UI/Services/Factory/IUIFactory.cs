using CodeBase.Infrastructure.Services;
using CodeBase.UI.Elements;

namespace CodeBase.UI.Services.Factory
{
  public interface IUIFactory: IService
  {
    void CreateUIRoot();
    void CreateShop();
  }
}