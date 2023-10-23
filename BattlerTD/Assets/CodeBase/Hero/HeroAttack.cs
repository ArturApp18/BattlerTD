using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using UnityEngine;
using PhysicsDebug = CodeBase.Logic.PhysicsDebug;

namespace CodeBase.Hero
{
  [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
  public class HeroAttack : MonoBehaviour, ISavedProgressReader
  {
    private const string Hittable = "Hittable";
    public HeroAnimator Animator;
    public CharacterController CharacterController;

    private IInputService _inputService;

    private static int _layerMask;
    private GameObject _impactVfx;
    private Collider[] _hits = new Collider[3];
    private Stats _stats;
    
    [SerializeField] private float Damage;
    [SerializeField] private GameObject ImpactFxPrefab;

    private void Awake()
    {
      _inputService = AllServices.Container.Single<IInputService>();

      _layerMask = 1 << LayerMask.NameToLayer(Hittable);
    }

    private void Update()
    {
      if(_inputService.IsAttackButtonUp() && !Animator.IsAttacking)
        Animator.PlayAttack();
    }

    public void LoadProgress(PlayerProgress progress)
    {
      _stats = progress.HeroStats;
    }

    private void OnAttack()
    {
      PhysicsDebug.DrawDebug(StartPoint() + transform.forward, _stats.DamageRadius, 1.0f);
      for (int i = 0; i < Hit(); ++i)
      {
        _hits[i].transform.GetComponentInParent<IHealth>().TakeDamage(Damage);
        PlayTakeDamageFx(_hits[i].transform.position);
      }
    }

    private int Hit() => 
      Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

    private Vector3 StartPoint() =>
      new Vector3(transform.position.x, CharacterController.center.y / 2, transform.position.z);

    private void PlayTakeDamageFx(Vector3 position) =>
      Instantiate(ImpactFxPrefab, position, Quaternion.identity);
  }
}