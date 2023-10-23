using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class GameRunner : MonoBehaviour
  {
    public GameBootstrapper BootstrapperPrefab;
    private void Awake()
    {
      GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();
      Debug.Log("QQQQQQQQQQQ");
      if(bootstrapper != null) return;

      Instantiate(BootstrapperPrefab);
    }
  }
}