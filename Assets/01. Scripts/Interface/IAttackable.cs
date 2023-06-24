public interface IAttackable
{
    AttackBehaviour CurAttackBehaviour { get; }
    void OnExecuteAttack(int attackIdx);
}
