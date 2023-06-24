public interface IAttackable
{
    AttackBehaviour AttackBehaviour { get; }
    void OnExecuteAttack(int attackIdx);
}
