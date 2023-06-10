using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    // 컨트롤러 속성 값
    public float speed = 5f;
    public float jumpHeight = 2f;
    public float dashDistance = 5f;
    public float gravity = -9.81f;
    public Vector3 drags;
    
    private Vector3 _calcVelocity;
    
    // Ground 체크
    public bool _isGround;
    
    // 컴포넌트
    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGround = _controller.isGrounded;

        if (_isGround && _calcVelocity.y < 0)
        {
            _calcVelocity.y = 0;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _controller.Move(move * (Time.deltaTime * speed));

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_isGround)
        {
            _calcVelocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            float value = (Mathf.Log(1f / (Time.deltaTime * drags.x + 1)) / -Time.deltaTime);
            Vector3 dashVelocity = Vector3.Scale(transform.forward, dashDistance * new Vector3(value,0f,value));
            _calcVelocity += dashVelocity;
        }

        // 중력 계산
        _calcVelocity.y += gravity * Time.deltaTime;
        
        // 대쉬의 drag 계산
        _calcVelocity.x /= 1 + drags.x * Time.deltaTime;
        _calcVelocity.y /= 1 + drags.y * Time.deltaTime;
        _calcVelocity.z /= 1 + drags.z * Time.deltaTime;

        _controller.Move(_calcVelocity * Time.deltaTime);
    }
}
