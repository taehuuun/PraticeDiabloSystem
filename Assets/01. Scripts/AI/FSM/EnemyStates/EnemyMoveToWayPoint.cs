using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveToWayPoint : State<EnemyController>
{
    private Animator _animator;
    private CharacterController _controller;
    private NavMeshAgent _agent;

    protected readonly int hashMove = Animator.StringToHash("Move");
    protected readonly int hashMoveSpeed = Animator.StringToHash("MoveSpeed");
    
    public override void OnInitialized()
    {
        _animator = context.GetComponent<Animator>();
        _controller = context.GetComponent<CharacterController>();
        _agent = context.GetComponent<NavMeshAgent>();
    }                                                       

    public override void OnEnter()
    {
        if (context.targetWayPoint == null)
        {
            context.FindNextWayPoint();
        }
        
        if (context.targetWayPoint)
        {
            _agent.SetDestination(context.targetWayPoint.position);
            _animator.SetBool(hashMove, true);
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
        else
        {
            if(!_agent.pathPending && (_agent.remainingDistance <= _agent.stoppingDistance))
            {
                Transform nextDest = context.FindNextWayPoint();

                if (nextDest)
                {
                    _agent.SetDestination(nextDest.position);
                }
                
                stateMachine.ChangeState<EnemyIdleState>();
            }
            else
            {
                _controller.Move(_agent.velocity * deltaTime);
                _animator.SetFloat(hashMoveSpeed, _animator.velocity.magnitude / _agent.speed, 0.1f, deltaTime);
            }
        }
    }

    public override void OnExit()
    {
        _animator.SetBool(hashMove, false);
        _agent.ResetPath();
    }
}
