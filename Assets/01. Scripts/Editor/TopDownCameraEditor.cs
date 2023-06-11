using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TopDownCamera))]
public class TopDownCameraEditor : Editor
{
    private TopDownCamera _topDownCamera;

    public override void OnInspectorGUI()
    {
        _topDownCamera = (TopDownCamera)target;
        base.OnInspectorGUI();
    }

    private void OnSceneGUI()
    {
        if (!_topDownCamera || !_topDownCamera.target)
        {
            return;
        }

        Transform cameraTarget = _topDownCamera.target;
        Vector3 targetPosition = cameraTarget.position;
        targetPosition.y += _topDownCamera.lookAtHeight;
        
        // Distance를 나타내는 구를 그림
        Handles.color = new Color(1f, 0f, 0f, 0.15f);
        Handles.DrawSolidDisc(targetPosition, Vector3.up, _topDownCamera.distance);
        
        Handles.color = new Color(0f, 1f, 0f, 0.75f);
        Handles.DrawWireDisc(targetPosition,Vector3.up,_topDownCamera.distance);
        
        // Handle의 Distance 슬라이더 추가
        Handles.color = new Color(1f, 0f, 0f, 0.5f);
        _topDownCamera.distance = Handles.ScaleSlider(_topDownCamera.distance, targetPosition, -cameraTarget.forward,
            Quaternion.identity, _topDownCamera.distance, 0.1f);
        _topDownCamera.distance = Mathf.Clamp(_topDownCamera.distance, 2f, float.MaxValue);
        
        // Handle의 Height 슬라이더 추가
        Handles.color = new Color(0f, 0f, 1f);
        _topDownCamera.height = Handles.ScaleSlider(_topDownCamera.height, targetPosition, Vector3.up,
            Quaternion.identity, _topDownCamera.height, 0.1f);
        _topDownCamera.height = Mathf.Clamp(_topDownCamera.height, 2f, float.MaxValue);

        // 라벨을 생성
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.fontSize = 15;
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.UpperCenter;
        
        // Distance 라벨
        Handles.Label(targetPosition + (-cameraTarget.forward * _topDownCamera.distance), "Distance", labelStyle);
        
        // Height 레벨
        labelStyle.alignment = TextAnchor.MiddleRight;
        Handles.Label(targetPosition + (Vector3.up * _topDownCamera.height),"Height",labelStyle);
        
        _topDownCamera.HandleCamera();
    }
}
