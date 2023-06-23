using UnityEngine;

public class EnemyIdleState : State<EnemyController>
{
    private Animator _animator;
    private CharacterController _controller;

    protected readonly int isMove = Animator.StringToHash("Move");
    protected readonly int moveSpeed = Animator.StringToHash("MoveSpeed");
    
    public override void OnInitialized()
    {
        _animator = context.GetComponent<Animator>();
        _controller = context.GetComponent<CharacterController>();
    }

    public override void OnEnter()
    {
        _animator?.SetBool(isMove, false);
        _animator?.SetFloat(moveSpeed, 0f);
        _controller?.Move(Vector3.zero);
    }

    public override void Update(float deltaTime)
    {
        Transform enemy = context.SearchEnemy();

        if (enemy)
        {
            if (context.IsAbleAttack)
            {
                stateMachine.ChangeState<EnemyAttackState>();
            }
            else
            {
                stateMachine.ChangeState<EnemyMoveState>();
            }
        }
    }
}
