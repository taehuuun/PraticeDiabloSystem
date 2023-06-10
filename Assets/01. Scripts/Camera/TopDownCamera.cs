using System;
using Unity.VisualScripting.ReorderableList;
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
        
        // 월드 포지션 벡터 생성
        Vector3 worldPositions = (Vector3.forward * -distance) + (Vector3.up * height);
        Debug.DrawLine(target.position, worldPositions, Color.red);
        
        // 회전 벡터
        Vector3 rotateVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPositions;
        Debug.DrawLine(target.position, rotateVector, Color.green);
        
        // 이동 벡터
        Vector3 finalTargetPosition = target.position;
        finalTargetPosition.y += lookAtHeight;

        Vector3 finalPosition = finalTargetPosition + rotateVector;
        Debug.DrawLine(target.position, finalPosition, Color.magenta);

        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, smoothSpeed);
        transform.LookAt(target.position);
    }
}
