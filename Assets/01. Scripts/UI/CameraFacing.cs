using UnityEngine;

public enum Axis { Up, Down, Left, Right, Forward}

public class CameraFacing : MonoBehaviour
{
    private Camera _referenceCamera;
    
    public bool reverseFace = false;
    public Axis axis = Axis.Up;

    private void Awake()
    {
        if (_referenceCamera == null)
        {
            _referenceCamera = Camera.main;
        }
    }
    
    public Vector3 GetAxis(Axis refAxis)
    {
        switch (refAxis)
        {
            case Axis.Down:
                return Vector3.down;
            case Axis.Left:
                return Vector3.left;
            case Axis.Right:
                return Vector3.right;
            case Axis.Forward:
                return Vector3.forward;
        }

        return Vector3.up;
    }
}
