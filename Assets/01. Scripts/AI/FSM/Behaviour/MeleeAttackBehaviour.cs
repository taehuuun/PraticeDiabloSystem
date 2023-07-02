using UnityEngine;

public class MeleeAttackBehaviour : AttackBehaviour
{
    public ManualCollision attackCollision;
    
    public override void ExecuteAttack(GameObject target = null, Transform startPoint = null)
    {
        Collider[] colliders = attackCollision?.CheckOverlapBox(targetMask);

        foreach (var collider in colliders)
        {
            if (collider.gameObject == target)
            {
                collider.GetComponent<IDamageable>()?.TakeDamage(damage,effect);
                return;
            }
        }
    }
}
