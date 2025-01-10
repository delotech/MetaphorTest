namespace metaphor.Scripts.Domain.Interface
{
    public interface ICharacter : ICombatant
    {
        int PlayerId { get; }
        string Name { get; }
        int Level { get; }
    }
}