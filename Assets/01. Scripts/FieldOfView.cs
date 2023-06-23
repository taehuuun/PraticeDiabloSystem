using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius = 5f;

    [Range(0, 360)] public float viewAngle = 90f;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    private List<Transform> _detectTargets = new List<Transform>();
    private Transform _nearestTarget;
    
    private float _distanceTarget;

    private void Update()
    {
        FindDetectTargets();
    }
    
    private void FindDetectTargets()
    {
        _distanceTarget = 0f;
        _nearestTarget = null;
        _detectTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;

            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distance, obstacleMask))
                {
                    _detectTargets.Add(target);

                    if (_nearestTarget == null || (_distanceTarget > distance))
                    {
                        _nearestTarget = target;
                        _distanceTarget = distance;
                    }
                }
            }
        }
    }
}
