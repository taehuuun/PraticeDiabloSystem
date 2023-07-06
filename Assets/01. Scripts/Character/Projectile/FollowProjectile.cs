using UnityEngine;

public class FollowProjectile : Projectile
{
    public float destroyDelayTime;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(DestoryParticle(destroyDelayTime));
    }

    protected override void FixedUpdate()
    {
        if (target)
        {
            Vector3 dest = target.transform.position;
            dest.y += 1.5f;
            transform.LookAt(dest);
        }
        
        base.FixedUpdate();
    }
}