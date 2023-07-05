using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public AttackBehaviour attackBehaviour;
    [HideInInspector] public GameObject owner;
    [HideInInspector] public GameObject target;
    public GameObject muzzlePrefab;
    public GameObject hitEffectPrefab;
    public AudioClip shotSfx;
    public AudioClip hitSfx;
    public float speed;

    private bool _isCollied;
    private Rigidbody _rigidbody;

    private void Start()
    {
        if (target)
        {
            Vector3 dest = target.transform.position;
            dest.y += 1.5f;
            transform.LookAt(dest);
        }
        
        if (owner)
        {
            Collider projectileCollider = GetComponent<Collider>();
            Collider[] ownerColliders = owner.GetComponentsInChildren<Collider>();

            foreach (var collider in ownerColliders)
            {
                Physics.IgnoreCollision(projectileCollider, collider);
            }
        }

        if (muzzlePrefab)
        {
            GameObject muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            
            ParticleSystem particle = muzzleVFX.GetComponent<ParticleSystem>();
            
            if (particle)
            {
                Destroy(muzzleVFX, particle.main.duration);
            }
            else
            {
                ParticleSystem childParticle = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();

                if (childParticle)
                {
                    Destroy(muzzleVFX,particle.main.duration);
                }
            }
        }

        if (shotSfx && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(shotSfx);
        }
    }
}