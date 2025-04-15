using System;
namespace DungeonExplorer
{
    public class Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public string Weakness { get; set; }

        public Enemy(string name, int health, int attack, string weakness)
        {
            Name = name;
            Health = health;
            Attack = attack;
            Weakness = weakness;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
            {
                Health = 0;
            }
        }
    }
}
