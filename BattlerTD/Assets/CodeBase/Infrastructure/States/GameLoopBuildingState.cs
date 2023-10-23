using CodeBase.Infrastructure.Services.Factory;

namespace CodeBase.Infrastructure.States
{
	public class GameLoopBuildingState : IState
	{
		private readonly IGameFactory _factory;

		public GameLoopBuildingState(IGameFactory factory)
		{
			_factory = factory;
		}

		public void Exit() =>
			_factory.HUD.DisappearTowerPanel();

		public void Update()
		{
			
		}

		public void Enter() =>
			_factory.HUD.AppearTowerPanel();
	}
}