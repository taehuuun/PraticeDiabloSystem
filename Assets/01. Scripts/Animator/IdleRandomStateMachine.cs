using UnityEngine;

public class IdleRandomStateMachine : StateMachineBehaviour
{
    public float minNormalizeTime = 2f;
    public float maxNormalizeTime = 5f;
    public float randomNormalizeTime;
    
    public int idleStates = 3;

    private readonly int _hashRandomIdle = Animator.StringToHash("RandomIdle");

     // OnStateEnter는 전환이 시작되고 상태 시스템이 이 상태를 평가하기 시작하면 호출됩니다
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 트랜지션의 필요한 시간을 랜덤하게 결정
        randomNormalizeTime = Random.Range(minNormalizeTime, maxNormalizeTime);
    }

    // OnStateEnter 콜백과 OnStateExit 콜백 사이의 각 업데이트 프레임에서 OnStateUpdate가 호출됩니다
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 이 State에서 벗어나 전환되는 경우 Idle 랜덤 매개 변수를 -1로 재설정합니다.
        if (animator.IsInTransition(0) &&
            animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash)
        {
            animator.SetInteger(_hashRandomIdle, -1);
        }
        
        // State가 Normalize 시간을 랜덤하게 결정 후 아직 전환 되지 않는 경우
        if (stateInfo.normalizedTime > randomNormalizeTime && !animator.IsInTransition(0))
        {
            animator.SetInteger(_hashRandomIdle, Random.Range(0,idleStates));
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
