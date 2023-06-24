using UnityEngine;
public interface IDamageable
{
    public bool IsAlive { get; }

    void TakeDamage(int damage, GameObject effect);
}
