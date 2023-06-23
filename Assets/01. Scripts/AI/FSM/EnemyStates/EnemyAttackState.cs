using UnityEngine;

public class EnemyAttackState : State<EnemyController>
{
    private Animator _animator;

    private int _hashAttack = Animator.StringToHash("Attack");

    public override void OnInitialized()
    {
        _animator = context.GetComponent<Animator>();
    }

    public override void OnEnter()
    {
        if (context.IsAbleAttack)
        {
            _animator.SetTrigger(_hashAttack);
        }
        else
        {
            stateMachine.ChangeState<EnemyIdleState>();
        }
    }

    public override void Update(float deltaTime)
    {
    }
}
