using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius = 5f;

    [Range(0, 360)] public float viewAngle = 90f;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> DetectTargets { get; private set; }
    private Transform _nearestTarget;
    
    private float _distanceTarget;

    private void Start()
    {
        DetectTargets = new List<Transform>();
    }
    
    private void Update()
    {
        FindDetectTargets();
    }
    
    private void FindDetectTargets()
    {
        _distanceTarget = 0f;
        _nearestTarget = null;
        DetectTargets.Clear();

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
                    DetectTargets.Add(target);

                    if (_nearestTarget == null || (_distanceTarget > distance))
                    {
                        _nearestTarget = target;
                        _distanceTarget = distance;
                    }
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        float angle = angleInDegrees * Mathf.Deg2Rad;

        return new Vector3(Mathf.Sin(angle), 0, MathF.Cos(angle));
    }
}
