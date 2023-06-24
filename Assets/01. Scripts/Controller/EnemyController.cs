using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public StateMachine<EnemyController> StateMachine { get; protected set; }

    private FieldOfView _fov;

    public Transform Target => _fov.Target;
    public Transform[] wayPoints;
    public Transform targetWayPoint = null;

    public float attackRange;
    public bool isPatrol;

    private int _wayPointIdx;

    public bool IsAbleAttack
    {
        get
        {
            if (!Target)
            {
                return false;
            }

            float distance = Vector3.Distance(transform.position, Target.position);

            return distance <= attackRange;
        }
    }
    
    protected virtual void Start()
    {
        _fov = GetComponent<FieldOfView>();
        State<EnemyController> startState = isPatrol ? new EnemyMoveToWayPoint() : new EnemyIdleState(false);
        StateMachine = new StateMachine<EnemyController>(this,startState);
        // StateMachine.AddState(new EnemyIdleState(isPatrol));
        // StateMachine.AddState(new EnemyMoveState());
        // StateMachine.AddState(new EnemyAttackState());
    }

    protected virtual void Update()
    {
        StateMachine.Update(Time.deltaTime);
    }

    public Transform SearchEnemy()
    {
        return Target;
    }
    public Transform FindNextWayPoint()
    {
        targetWayPoint = null;

        if (wayPoints.Length > 0)
        {
            targetWayPoint = wayPoints[_wayPointIdx];
        }

        _wayPointIdx = (_wayPointIdx + 1) % wayPoints.Length;
        
        return targetWayPoint;
    }
}
