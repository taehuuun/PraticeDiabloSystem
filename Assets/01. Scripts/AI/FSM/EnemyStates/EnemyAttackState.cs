using UnityEngine;

public class EnemyAttackState : State<EnemyController>
{
    private Animator _animator;
    private AttackStateController _attackStateController;
    private IAttackable _attackable;

    private int _hashAttack = Animator.StringToHash("Attack");
    private int _hashAttackIdx = Animator.StringToHash("AttackIdx");

    public override void OnInitialized()
    {
        _animator = context.GetComponent<Animator>();
        _attackStateController = context.GetComponent<AttackStateController>();
        _attackable = context.GetComponent<IAttackable>();
    }

    public override void OnEnter()
    {
        if (_attackable ==null || Equals(_attackable.CurAttackBehaviour, null))
        {
            stateMachine.ChangeState<EnemyIdleState>();
            return;
        }

        _attackStateController.enterAttackStateController += OnEnterAttackState;
        _attackStateController.exitAttackStateController += OnExitAttackState;
        
        _animator?.SetInteger(_hashAttackIdx, _attackable.CurAttackBehaviour.animationIdx);
        _animator.SetTrigger(_hashAttack);
    }
    
    public override void Update(float deltaTime)
    {
    }

    public void OnEnterAttackState()
    {
        
    }

    public void OnExitAttackState()
    {
        stateMachine.ChangeState<EnemyIdleState>();
    }
}
