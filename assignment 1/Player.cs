using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player : Character, IDamage
    {
        public int Attack { get; set; }
        public List<Weapon> Inventory { get; set; }

        public Player(string name, int health)
            : base(name, health)
        {
            Attack = 10;
            Inventory = new List<Weapon>();
        }

        public void ShowStatus()
        {
            Console.WriteLine(Name + " - Health: " + Health + ", Attack: " + Attack);
            Console.WriteLine("Inventory:");
            if (Inventory.Count == 0)
            {
                Console.WriteLine("- (empty)");
            }
            else
            {
                foreach (Weapon weapon in Inventory)
                {
                    Console.WriteLine("- " + weapon.Name + " (Damage: " + weapon.BaseDamage + ")");
                }
            }
        }

        public void PickUpItem(Weapon weapon)
        {
            Inventory.Add(weapon);
            Console.WriteLine("Picked up: " + weapon.Name);
        }

        public void AttackEnemy(Enemy enemy, Weapon weapon)
        {
            int damage = weapon.BaseDamage;

            if (enemy.Weakness == weapon.Name)
            {
                Console.WriteLine(enemy.Name + " is vulnerable to " + weapon.Name + "! Critical damage!");
                damage = 2000;
            }

            Console.WriteLine(Name + " attacks " + enemy.Name + " with " + weapon.Name + " for " + damage + " damage.");
            enemy.DamageTaken(damage);

            if (enemy.Health <= 0)
            {
                Console.WriteLine(enemy.Name + " was defeated!");
            }
        }

        public void UseHealthKit()
        {
            Health = Health + 20;
            if (Health > 100)
            {
                Health = 100;
            }
            Console.WriteLine(Name + " used a Health Kit and healed to " + Health + " HP!");
        }

        public void DamageTaken(int damage)
        {
            Health = Health - damage;
            if (Health <= 0)
            {
                Health = 0;
                Console.WriteLine("You have been defeated!");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Your current health: " + Health);
            }
        }
    }
}

