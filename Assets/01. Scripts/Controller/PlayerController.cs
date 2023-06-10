using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // 컨트롤러 속성 값
    private Vector3 _calcVelocity;
    
    // Ground 체크
    public LayerMask groundLayerMast;
    public float groundCheckDistance = 0.3f;
    private bool _isGround;
    
    // 컴포넌트
    private CharacterController _controller;
    private NavMeshAgent _agent;
    private Camera _camera;

    private void Start()
    {
        // CharacterController 컴포넌트를 얻음
        _controller = GetComponent<CharacterController>();
        
        // NavMeshAgent 컴포넌트를 얻음
        _agent = GetComponent<NavMeshAgent>();
        _agent.updatePosition = false;
        _agent.updateRotation = true;
        
        // 메인 카메라를 얻음
        _camera = Camera.main;
    }

    private void Update()
    {
        // 사용자가 마우스 왼쪽 클릭 했을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 카메라 피킹 사용 
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, groundLayerMast))
            {
            }
        }
    }
}
