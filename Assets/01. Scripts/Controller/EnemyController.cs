using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public StateMachine<EnemyController> StateMachine { get; protected set; }

    public Transform target;
    public LayerMask targetLayerMask;
    public float viewRadius;
    public float attackRange;

    public bool IsAbleAttack
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
        Gizmos.DrawWireSphere(curPosition, viewRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(curPosition, attackRange);
    }

    public Transform SearchEnemy()
    {
        target = null;
        Collider[] targetViews = Physics.OverlapSphere(transform.position, viewRadius,targetLayerMask);

        if (targetViews.Length > 0)
        {
            target = targetViews[0].transform;
        }

        return target;
    }
}
