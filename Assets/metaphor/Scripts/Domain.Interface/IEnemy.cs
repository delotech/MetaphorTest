namespace metaphor.Scripts.Domain.Interface
{
    public interface IEnemy : ICombatant
    {
        int EnemyId { get; }
        string Name { get; }
        int Level { get; }
    }
}