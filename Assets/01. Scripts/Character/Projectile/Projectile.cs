using System.Collections;
using System.Collections.Generic;
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

    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
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

    private void FixedUpdate()
    {
        if (speed != 0 && _rigidbody)
        {
            _rigidbody.position += (transform.forward) * (speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollied)
        {
            return;
        }

        _isCollied = true;

        Collider projectileCollider = GetComponent<Collider>();
        projectileCollider.enabled = false;

        if (hitSfx && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(hitSfx);
        }

        speed = 0;
        _rigidbody.isKinematic = true;

        ContactPoint contact = collision.contacts[0];
        Quaternion contactRotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 contactPosition = contact.point;

        if (hitEffectPrefab)
        {
            GameObject hitVFX = Instantiate(hitEffectPrefab,contactPosition,contactRotation);
            ParticleSystem particle = hitVFX.GetComponent<ParticleSystem>();
            
            if (particle)
            {
                Destroy(hitVFX, particle.main.duration);
            }
            else
            {
                ParticleSystem childParticle = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();

                if (childParticle)
                {
                    Destroy(hitVFX,particle.main.duration);
                }
            }
        }

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable!=null)
        {
            damageable.TakeDamage(attackBehaviour?.damage ?? 0, null);
        }

        StartCoroutine(DestoryParticle(0.0f));
    }

    private IEnumerator DestoryParticle(float waitTime)
    {
        if (transform.childCount > 0 && waitTime != 0)
        {
            List<Transform> childs = new List<Transform>();

            foreach (Transform child in transform.GetChild(0).transform)
            {
                childs.Add(child);
            }

            while (transform.GetChild(0).localScale.x > 0)
            {
                yield return new WaitForSeconds(0.01f);

                transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);

                for (int i = 0; i < childs.Count; i++)
                {
                    childs[i].localScale-= new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }

        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}