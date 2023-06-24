using UnityEngine;

public class EnemyDeadState : State<EnemyController>
{
    private Animator _animator;

    protected int hashIsAlive = Animator.StringToHash("isAlive");
    
    public override void OnInitialized()
    {
        _animator = context.GetComponent<Animator>();
    }

    public override void OnEnter()
    {
        _animator.SetBool(hashIsAlive, false);
    }

    public override void Update(float deltaTime)
    {
        if (stateMachine.ElapsedTimeInState > 3.0f)
        {
            GameObject.Destroy(context.gameObject);
        }
    }
}
