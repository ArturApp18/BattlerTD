using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Timers;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
  public abstract class WindowBase : MonoBehaviour
  {
    [SerializeField] protected List<Button> CloseButton;
    
    protected IPersistentProgressService ProgressService;
    protected ITimerService Timer;
    protected PlayerProgress Progress => ProgressService.Progress;

    public void Construct(IPersistentProgressService progressService, ITimerService timer)
    {
      ProgressService = progressService;
      Timer = timer;
    }

    private void Awake() => 
      OnAwake();

    private void Start()
    {
      Initialize();
      SubscribeUpdates();
    }

    private void OnDestroy() => 
      Cleanup();

    protected virtual void OnAwake()
    {
      foreach (Button button in CloseButton)
      {
        button.onClick.AddListener(() =>
        {
          Timer.StartTimer();
          Time.timeScale = 1;
          Destroy(gameObject);
        });
        
      }
    }

    protected virtual void Initialize(){}
    protected virtual void SubscribeUpdates(){}
    protected virtual void Cleanup(){}
  }
}