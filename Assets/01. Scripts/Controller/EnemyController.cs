using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public StateMachine<EnemyController> StateMachine { get; protected set; }

    private FieldOfView _fov;

    public Transform Target => _fov.Target;

    // public Transform target;
    // public LayerMask targetLayerMask;
    // public float viewRadius;
    public float attackRange;

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
    
    private void Start()
    {
        _fov = gameObject.GetComponent<FieldOfView>();
        StateMachine = new StateMachine<EnemyController>(this,new EnemyIdleState());
        StateMachine.AddState(new EnemyMoveState());
        StateMachine.AddState(new EnemyAttackState());
    }

    private void Update()
    {
        StateMachine.Update(Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Vector3 curPosition = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(curPosition, _fov.viewRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(curPosition, attackRange);
    }

    public Transform SearchEnemy()
    {
        return Target;
    }
}
