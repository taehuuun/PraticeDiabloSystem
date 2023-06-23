using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius = 5f;

    [Range(0, 360)] public float viewAngle = 90f;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> detectTargets = new List<Transform>();
    public Transform Target { get; private set;}

    public float delay = 0.2f;
    
    private float _distanceTarget;

    private void Start()
    {
        StartCoroutine(FindDetectTargetsDelay(delay));
    }
    
    private IEnumerator FindDetectTargetsDelay(float delayTime)
    {
        while (true)
        {
            FindDetectTargets();
            yield return new WaitForSeconds(delayTime);
        }
    }

    private void FindDetectTargets()
    {
        _distanceTarget = 0f;
        Target = null;
        detectTargets.Clear();

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
                    detectTargets.Add(target);

                    if (Target == null || (_distanceTarget > distance))
                    {
                        Target = target;
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
