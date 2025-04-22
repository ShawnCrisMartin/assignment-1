using System;

namespace DungeonExplorer
{
    public class Enemy : IDamage
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Damage { get; set; }
        public string Weakness { get; set; }

        public Enemy(string name, int health, int damage, int attack, string weakness)
        {
            Name = name;
            Health = health;
            Attack = attack;
            Damage = damage;
            Weakness = weakness;
        }

        public void DamageTaken(int damage)
        {
            Health -= damage;
            if (Health < 0)
            {
                Health = 0;
            }
        }

        // Enemy attacks player
        public void AttackPlayer(Player player)
        {
            player.DamageTaken(Damage);
            Console.WriteLine($"{Name} attacks {player.Name} for {Damage} damage!");
        }
    }
}
