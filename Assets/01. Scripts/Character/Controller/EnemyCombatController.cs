using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : EnemyController, IAttackable, IDamageable
{
    // 필드
    [SerializeField] private List<AttackBehaviour> attackBehaviours = new List<AttackBehaviour>();
    [SerializeField] private GaugeUI hpUI;
    public Transform projectileTransform;
    public Transform hitTransform;
    public LayerMask targetMask;
    private Animator _animator;
    
    public AttackBehaviour CurAttackBehaviour { get; private set; }
    public int Health { get; private set; }
    
    public int maxHealth;
    public bool IsAlive => Health > 0;
    
    private readonly int _hashHitTrigger = Animator.StringToHash("Hit");
    
    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
        StateMachine.AddState(new EnemyMoveState());
        StateMachine.AddState(new EnemyAttackState());
        InitAttackBehaviour();

        Health = maxHealth;
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
            CurAttackBehaviour.ExecuteAttack(Target.gameObject, projectileTransform);
        }
    }

    public void TakeDamage(int damage, GameObject effect)
    {
        if (!IsAlive)
        {
            return;
        }

        Health -= damage;

        if (effect)
        {
            Instantiate(effect, hitTransform);
        }

        if (IsAlive)
        {
            _animator?.SetTrigger(_hashHitTrigger);
        }
        else
        {
            StateMachine.ChangeState<EnemyDeadState>();
        }
    }

    private void InitAttackBehaviour()
    {
        foreach (var behaviour in attackBehaviours)
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

            foreach (var attackBehaviour in attackBehaviours)
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
