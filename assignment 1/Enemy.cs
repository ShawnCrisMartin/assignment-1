using System;

namespace DungeonExplorer
{
    // enemy class including all enemy characters and It inherits from Character which holds name and health and implements the IDamage interface so it can take damage.
    public class Enemy : Character, IDamage
    {
        // how strong the enemy’s attack stats used for calculations, of subtracting from players health after attack
        public int Attack { get; set; }
        public int Damage { get; set; }

        // the weapon this enemy is weak against
        public string Weakness { get; set; }

        // constructor to create a new enemy with custom values
        public Enemy(string name, int health, int damage, int attack, string weakness)
            : base(name, health) 
        {
            Attack = attack;
            Damage = damage;
            Weakness = weakness;
        }

        // reduces enemies health when attacked
        public void DamageTaken(int damage)
        {
            Health -= damage;

            // health wont drop below zero becaause of this, if it does it is set again to 0
            if (Health < 0)
            {
                Health = 0;
            }
        }

        // this method lets the enemy attack the player
        public void AttackPlayer(Player player)
        {
            // calls the players damagetaken method to reduce their health
            player.DamageTaken(Damage);

         
            Console.WriteLine(Name + " attacking " + player.Name + " for " + Damage + " damage");
        }
    }
}
