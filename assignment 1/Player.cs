using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player : Characters
    {
        private List<string> _inventory;

        public Player(string name, int health) : base(name, health)
        {
            _inventory = new List<string>();
        }

        public void PickUpItem(string item)
        {
            if (!string.IsNullOrWhiteSpace(item))
            {
                _inventory.Add(item);
                Console.WriteLine($"You picked up: {item}");
            }
            else
            {
                Console.WriteLine("That item doesn't seem to exist.");
            }
        }

        public void ShowInventory()
        {
            Console.WriteLine("\n--- Inventory ---");

            if (_inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
                return;
            }

            foreach (var item in _inventory)
            {
                Console.WriteLine($"- {item}");
            }
        }

        public void ShowStatus()
        {
            Console.WriteLine("\n--- Player Status ---");
            Console.WriteLine($"Name   : {CharacterName}");
            Console.WriteLine($"Health : {CharacterHealth}");
            Console.WriteLine("----------------------");
        }

        public void TakeDamage(int amount)
        {
            CharacterHealth -= amount;

            if (CharacterHealth < 0)
                CharacterHealth = 0;

            Console.WriteLine($"{CharacterName} took {amount} damage! Health is now {CharacterHealth}.");
        }

        public void Heal(int amount)
        {
            CharacterHealth += amount;
            Console.WriteLine($"{CharacterName} healed for {amount} points. Health is now {CharacterHealth}.");
        }
    }
}
