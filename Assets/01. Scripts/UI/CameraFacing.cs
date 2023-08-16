using UnityEngine;

public enum Axis { Up, Down, Left, Right, Forward}

public class CameraFacing : MonoBehaviour
{
    private Camera _referenceCamera;
    
    public bool reverseFace = false;
    public Axis axis = Axis.Up;
    
}
