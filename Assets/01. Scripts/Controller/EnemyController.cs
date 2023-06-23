using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected StateMachine<EnemyController> stateMachine;

    public Transform target;
    public LayerMask targetLayerMask;
    public float viewRadius;
    public float attackRange;

    public bool isAbleAttack
    {
        get
        {
            if (!target)
            {
                return false;
            }

            float distance = Vector3.Distance(transform.position, target.position);

            return distance <= attackRange;
        }
    }
    
    private void Start()
    {
        stateMachine = new StateMachine<EnemyController>(this,new EnemyIdleState());
        stateMachine.AddState(new EnemyMoveState());
        stateMachine.AddState(new EnemyAttackState());
    }

    private void Update()
    {
        stateMachine.Update(Time.deltaTime);
    }

    public Transform SearchEnemy()
    {
        target = null;
        Collider[] targetViews = null;
        Physics.OverlapSphereNonAlloc(transform.position, viewRadius, targetViews, targetLayerMask);

        if (targetViews.Length > 0)
        {
            target = targetViews[0].transform;
        }

        return target;
    }
}
