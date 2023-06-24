using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : EnemyController, IAttackable, IDamageable
{
    // 필드
    public Transform attackPoint;
    public LayerMask targetMask;
    
    private List<AttackBehaviour> _attackBehaviours = new List<AttackBehaviour>();
    
    // 프로퍼티
    public AttackBehaviour CurAttackBehaviour { get; private set; }
    public bool IsAlive { get; }
    
    protected override void Start()
    {
        base.Start();
        StateMachine.AddState(new EnemyMoveState());
        StateMachine.AddState(new EnemyAttackState());
        InitAttackBehaviour();
    }

    protected override void Update()
    {
        CheckAttackBehaviour();
        base.Update();
    }

    public void OnExecuteAttack(int attackIdx)
    {
        if (CurAttackBehaviour != null && Target != null)
        {
            CurAttackBehaviour.ExecuteAttack(Target.gameObject, attackPoint);
        }
    }

    public void TakeDamage(int damage, GameObject effect)
    {
    }

    private void InitAttackBehaviour()
    {
        foreach (var behaviour in _attackBehaviours)
        {
            if (CurAttackBehaviour == null)
            {
                CurAttackBehaviour = behaviour;
            }

            behaviour.targetMask = targetMask;
        }
    }

    private void CheckAttackBehaviour()
    {
        if (CurAttackBehaviour == null || !CurAttackBehaviour.IsValilalbe)
        {
            CurAttackBehaviour = null;

            foreach (var attackBehaviour in _attackBehaviours)
            {
                if (attackBehaviour.IsValilalbe)
                {
                    if ((CurAttackBehaviour == null) || CurAttackBehaviour.priority < attackBehaviour.priority)
                    {
                        CurAttackBehaviour = attackBehaviour;
                    }
                }
            }
        }
    }
}
