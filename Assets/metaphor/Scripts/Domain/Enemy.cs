using metaphor.Scripts.Domain.Interface;

namespace metaphor.Scripts.Domain
{
    public class Enemy : IEnemy
    {
        public int EnemyId { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Weapon { get;private set; }
        public int Armour { get; private set; }
        public int MaxHealthPoints { get; private set; }
        public int HealthPoints { get; private set; }
        public int Strength { get; private set; }
        public int Endurance { get; private set; }
        
        // Stats
        public bool Died => HealthPoints <= 0;
        public bool Defending { get; private set; }
        
        public Enemy(int enemyId, string name, int level, int healthPoints, int maxHealthPoints, int strength, int endurance)
        {
            EnemyId = enemyId;
            Name = name;
            Level = level;
            HealthPoints = healthPoints;
            MaxHealthPoints = maxHealthPoints;
            Strength = strength;
            Endurance = endurance;
        }
        
        public void Attack()
        {
            HealthPoints -= 10;

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