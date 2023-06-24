public class EnemyCombatController : EnemyController
{
    protected override void Start()
    {
        base.Start();
        StateMachine.AddState(new EnemyMoveState());
        StateMachine.AddState(new EnemyAttackState());
    }
}
