using UnityEngine;

public class EnemyIdleState : State<EnemyController>
{
    private Animator _animator;
    private CharacterController _controller;

    protected int isMove = Animator.StringToHash("Move");
    protected int moveSpeed = Animator.StringToHash("MoveSpeed");
    
    public override void OnInitialized()
    {
        _animator = context.GetComponent<Animator>();
        _controller = context.GetComponent<CharacterController>();
    }

    public override void OnEnter()
    {
        _animator?.SetBool(isMove, false);
    }

    public override void Update(float deltaTime)
    {
    }
}
