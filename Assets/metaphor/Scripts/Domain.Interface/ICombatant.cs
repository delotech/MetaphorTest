namespace metaphor.Scripts.Domain.Interface
{
    public interface ICombatant
    {
        // Equipments
        int Weapon { get; }
        int Armour { get; }
        
        // Stats
        int HealthPoints { get; }
        int MaxHealthPoints { get; }
        int Strength { get; }
        int Endurance { get; }
        
        // Actions
        void Attack();
        void Defend();
        
        // Stats
        bool Defending { get; }
        bool Died { get; }
    }
}