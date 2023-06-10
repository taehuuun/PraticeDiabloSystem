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
    
    // Animator
    private readonly int _moveHash = Animator.StringToHash("Move");
    private readonly int _fallingHash = Animator.StringToHash("Falling");
    
    // 컴포넌트
    private CharacterController _controller;
    private NavMeshAgent _agent;
    private Camera _camera;
    private Animator _animator;

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

        _animator = GetComponent<Animator>();
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
                _agent.SetDestination(hit.point);
            }

            bool isMoving = _agent.remainingDistance > _agent.stoppingDistance;
            
            Vector3 motion = isMoving ? _agent.velocity * Time.deltaTime : Vector3.zero; 
            _animator.SetBool(_moveHash,isMoving);
            
            _controller.Move(motion);
        }
    }

    private void LateUpdate()
    {
        transform.position = _agent.nextPosition;
    }
}
