using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    // this is the Player class and it inherits from Character and implements IDamage interface.
    public class Player : Character, IDamage
    {
        // players attack values 
        public int Attack { get; set; }

        // list that holds the players inventory of weapons
        public List<Weapon> Inventory { get; set; }

        // initializing player with a name and health, this is the constructor that initializes the name and health
        public Player(string name, int health)
            : base(name, health)
        {
            Attack = 10; // default attack value is set as 10
            Inventory = new List<Weapon>();
        }

        // function that displays the players current status, which are health, attack, and inventory
        public void ShowStatus()
        {
            Console.WriteLine(Name + " - Health: " + Health + ", Attack: " + Attack);
            Console.WriteLine("Inventory:");

            // checking if empty or not
            if (Inventory.Count == 0)
            {
                Console.WriteLine("- (empty)");
            }
            else
            {
                // shows all the weapons in the inventory, everything that is picked up inclcuding healthkit
                foreach (Weapon weapon in Inventory)
                {
                    Console.WriteLine("- " + weapon.Name + " (Damage: " + weapon.BaseDamage + ")");
                }
            }
        }

        // add the weapon or picked up item to the inventory of the player
        public void PickUpItem(Weapon weapon)
        {
            if (weapon != null)
            {
                Inventory.Add(weapon);
                Console.WriteLine("Picked up: " + weapon.Name);
            }
        }

        // function that is used in attacking the enemy, can select the weapon to attack 
        public void AttackEnemy(Enemy enemy, Weapon weapon)
        {
            // checks if enemy or weapon is null
            if (enemy == null || weapon == null)
            {
                Console.WriteLine("invalid enemy/weapon!");
                return;
            }

            // initial damage is set to weapons basedamage
            int damage = weapon.BaseDamage;

            // if enemy has a weakness to the selected weapon, instantly dead
            if (enemy.Weakness == weapon.Name)
            {
                Console.WriteLine(enemy.Name + " has a weakness of" + weapon.Name + "! Critical damage");
                damage = 2000;
            }

            // attack info
            Console.WriteLine(Name + " attacks " + enemy.Name + " with " + weapon.Name + " for " + damage + " damage");

            // apply damage to the enemy
            enemy.DamageTaken(damage);

            // checking if enemy is dead or not
            if (enemy.Health <= 0)
            {
                Console.WriteLine(enemy.Name + " was defeated");
            }
        }

        // health kit and healing method
        public void UseHealthKit()
        {
            // only heal if health is below 100
            if (Health < 100)
            {
                Health += 20;
                if (Health > 100)
                {
                    Health = 100; // this is to limit the health from exceeding above 100
                }
                Console.WriteLine(Name + " you are using the health kit... " + Health + " HP!");
            }
            else
            {
                Console.WriteLine("health is already full....dont use the health kit now...");
            }
        }

        // method for taking damage when attacked
        public void DamageTaken(int damage)
        {
            Health -= damage;

            // if health drops to 0 or below, end the game
            if (Health <= 0)
            {
                Health = 0;
                Console.WriteLine("you are dead...bye");
                Environment.Exit(0); // ending the game
            }
            else
            {
                Console.WriteLine(" current health is: " + Health);
            }
        }
    }
}

