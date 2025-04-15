using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public List<Weapon> Inventory { get; set; }

        public Player(string name, int health)
        {
            Name = name;
            Health = health;
            Attack = 10; // default
            Inventory = new List<Weapon>();
        }

        public void ShowStatus()
        {
            Console.WriteLine($"{Name} - Health: {Health}, Attack: {Attack}");
            Console.WriteLine("Inventory:");
            if (Inventory.Count == 0)
            {
                Console.WriteLine("- (empty)");
            }
            else
            {
                foreach (var weapon in Inventory)
                {
                    Console.WriteLine($"- {weapon.Name} (Damage: {weapon.BaseDamage})");
                }
            }
        }

        public void PickUpItem(Weapon weapon)
        {
            Inventory.Add(weapon);
            Console.WriteLine($"Picked up: {weapon.Name}");
        }

        public void AttackEnemy(Enemy enemy, Weapon weapon)
        {
            int damage = weapon.BaseDamage;

            if (weapon.Name.Equals(enemy.Weakness, StringComparison.OrdinalIgnoreCase))
            {
                damage = enemy.Health;
                Console.WriteLine($"Critical hit! {weapon.Name} is the weakness of {enemy.Name}.");
            }
            else
            {
                Console.WriteLine($"{Name} attacks {enemy.Name} with {weapon.Name} for {damage} damage.");
            }

            enemy.TakeDamage(damage);
        }
    }
}

