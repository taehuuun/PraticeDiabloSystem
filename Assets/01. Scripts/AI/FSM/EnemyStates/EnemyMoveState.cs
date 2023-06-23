using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveState : State<EnemyController>
{
    private Animator _animator;
    private CharacterController _controller;
    private NavMeshAgent _agent;

    private int _hashMove = Animator.StringToHash("Move");
    private int _hashMoveSpeed = Animator.StringToHash("MoveSpeed");
    
    public override void OnInitialized()
    {
        _animator = context.GetComponent<Animator>();
        _controller = context.GetComponent<CharacterController>();
        _agent = context.GetComponent<NavMeshAgent>();
    }

    public override void OnEnter()
    {
        _agent.SetDestination(context.Target.position);
        _animator.SetBool(_hashMove, true);
    }

    public override void Update(float deltaTime)
    {
        Transform enemy = context.SearchEnemy();

        if (enemy)
        {
            _agent.SetDestination(context.Target.position);

            if (_agent.remainingDistance > _agent.stoppingDistance)
            {
                _controller.Move(_agent.velocity * Time.deltaTime);
                _animator.SetFloat(_hashMoveSpeed, _agent.velocity.magnitude / _agent.speed,1f, deltaTime);
                return;
            }
        }

        if (!enemy || _agent.remainingDistance <= _agent.stoppingDistance)
        {
            stateMachine.ChangeState<EnemyIdleState>();
        }
    }

    public override void OnExit()
    {
        _animator.SetBool(_hashMove, false);
        _animator.SetFloat(_hashMoveSpeed, 0f);
        _agent.ResetPath();
    }
}
