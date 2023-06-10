using System;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public float height = 5f;
    public float distance = 10f;
    public float angle = 45f;
    public float lookAtHeight = 2f;
    public float smoothSpeed = 0.5f;

    public Transform target;
    
    private Vector3 refVelocity;

    private void LateUpdate()
    {
        HandleCamera();
    }

    private void HandleCamera()
    {
        if (!target)
        {
            return;
        }

        Vector3 worldPositions = (Vector3.forward * -distance) + (Vector3.up * height);
        Debug.DrawLine(target.position, worldPositions, Color.red);
    }
}
