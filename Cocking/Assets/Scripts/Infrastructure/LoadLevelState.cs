using UnityEngine;

namespace Infrastructure
{
	public class LoadLevelState : IPayloadState<string>
	{
		private const string InitialPoint = "InitialPoint";
		private readonly GameStateMachine _gameStateMachine;
		private readonly SceneLoader _sceneLoader;

		public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
		{
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
		}

		public void Enter(string sceneName) =>
			_sceneLoader.Load(sceneName, OnLoaded);

		public void Exit()
		{
			
		}

		private void OnLoaded()
		{
			var initialPoint = GameObject.FindWithTag(InitialPoint);
			
			GameObject hero = Instantiate("Hero/Player", initialPoint.transform.position);
			Instantiate("UI/Hud/Hud");
		}

		private static GameObject Instantiate(string path)
		{
			var heroPrefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(heroPrefab);
			
		}
		
		private static GameObject Instantiate(string path, Vector3 at)
		{
			var heroPrefab = Resources.Load<GameObject>(path); 
			return Object.Instantiate(heroPrefab, at, Quaternion.identity);
			
		}

		public void Update()
		{
			throw new System.NotImplementedException();
		}
	}
}