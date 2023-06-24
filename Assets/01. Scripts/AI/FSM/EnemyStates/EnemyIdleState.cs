using UnityEngine;

public class EnemyIdleState : State<EnemyController>
{
    private Animator _animator;
    private CharacterController _controller;

    protected readonly int isMove = Animator.StringToHash("Move");
    protected readonly int moveSpeed = Animator.StringToHash("MoveSpeed");
    
    private bool _isPatrol;
    private float _minIdleTime = 0f;
    private float _maxIdleTime = 3.0f;
    private float _idleTime = 0f;
    
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

        if (_isPatrol)
        {
            _idleTime = Random.Range(_minIdleTime, _maxIdleTime);
        }
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
        else if(_isPatrol && stateMachine.ElapsedTimeInState > _idleTime)
        {
            stateMachine.ChangeState<EnemyMoveToWayPoint>();
        }
    }
}
