using UnityEngine;

public class ProjectileAttackBehaviour : AttackBehaviour
{
    public override void ExecuteAttack(GameObject target = null, Transform startPoint = null)
    {
        if (!target)
        {
            return;
        }

        Vector3 projectilePosition = startPoint?.position ?? transform.position;

        if (effect)
        {
            Projectile projectile = Instantiate(effect, projectilePosition, Quaternion.identity).GetComponent<Projectile>();
            projectile.transform.forward = transform.forward;

            if (projectile)
            {
                projectile.owner = this.gameObject;
                projectile.target = target;
                projectile.attackBehaviour = this;
            }
        }

        calCoolTime = 0.0f;
    }
}