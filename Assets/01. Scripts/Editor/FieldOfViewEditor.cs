using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        
        Vector3 fovPosition = fov.transform.position;
        
        Handles.color = Color.black;
        Handles.DrawWireArc(fovPosition, Vector3.up,Vector3.forward,360,fov.viewRadius);

        float angle = fov.viewAngle / 2;

        Vector3 viewAngleA = fov.DirFromAngle(-angle, false);
        Vector3 viewAngleB = fov.DirFromAngle(angle, false);
        
        Handles.DrawLine(fovPosition, fovPosition + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fovPosition, fovPosition + viewAngleB * fov.viewRadius);

        Handles.color = Color.red;

        foreach (Transform detectTarget in fov.detectTargets)
        {
            Handles.DrawLine(fovPosition,detectTarget.position);
        }
    }
}
