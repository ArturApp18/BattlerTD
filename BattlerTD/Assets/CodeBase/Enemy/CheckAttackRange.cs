using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(EnemyAttack))]
  public class CheckAttackRange : MonoBehaviour
  {
    public EnemyAttack _enemyAttack;
    public TriggerObserver TriggerObserver;

    private void Start()
    {
      TriggerObserver.TriggerEnter += TriggerEnter;
      TriggerObserver.TriggerStay += TriggerStay;
      TriggerObserver.TriggerExit += TriggerExit;
      
      _enemyAttack.DisableAttack();
    }

    private void OnDestroy()
    {
      TriggerObserver.TriggerEnter -= TriggerEnter;
      TriggerObserver.TriggerStay -= TriggerStay;
      TriggerObserver.TriggerExit -= TriggerExit;
    }

    private void TriggerStay(Collider obj)
    {
      
    }

    private void TriggerExit(Collider obj)
    {
      _enemyAttack.DisableAttack();
    }

    private void TriggerEnter(Collider obj)
    {
      _enemyAttack.EnableAttack();
    }
  }
}