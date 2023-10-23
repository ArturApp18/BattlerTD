using UnityEngine.SceneManagement;

namespace UI
{
	public static class Loader
	{
		public static void Load(string scene)
		{
			SceneManager.LoadScene(scene);
		}
	}
}