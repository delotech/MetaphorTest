using metaphor.Scripts.Domain.Interface;

namespace metaphor.Scripts.Domain
{
    public class Character : ICharacter
    {
        public int PlayerId { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Weapon { get; private set; }
        public int Armour { get; private set; }
        public int HealthPoints { get; private set; }
        public int MaxHealthPoints { get; private set; }
        public int Strength { get; private set; }
        public int Endurance { get; private set; }

        // stats
        public bool Died => HealthPoints <= 0;
        public bool Defending { get; private set; }

        public Character(int playerId, string name, int level, int weapon, int armour,
            int healthPoints, int maxHealthPoints, int strength,int endurance)
        {
            PlayerId = playerId;
            Name = name;
            Level = level;
            Weapon = weapon;
            Armour = armour;
            HealthPoints = healthPoints;
            MaxHealthPoints = maxHealthPoints;
            Strength = strength;
            Endurance = endurance;
        }
        
        public void Attack()
        {
            HealthPoints -= 15;

            if (Died)
            {
                // do something
            }
        }

        public void Defend()
        {
            Defending = true;
        }

      
    }
}