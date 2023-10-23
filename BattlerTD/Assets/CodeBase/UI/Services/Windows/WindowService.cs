using CodeBase.Infrastructure.Services.Timers;
using CodeBase.UI.Services.Factory;

namespace CodeBase.UI.Services.Windows
{
	public class WindowService : IWindowService
	{
		private readonly IUIFactory _uiFactory;
		private readonly ITimerService _timer;

		public WindowService(IUIFactory uiFactory, ITimerService timer)
		{
			_uiFactory = uiFactory;
			_timer = timer;
		}

		public void Open(WindowId windowId)
		{
			switch (windowId)
			{
				case WindowId.None:
					break;
				case WindowId.Shop:
					_uiFactory.CreateShop();
					break;
				case WindowId.TowerPanel:
					_timer.StopTimer();
					break;
			}
		}
	}
}