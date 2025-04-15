using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Game
    {
        private Player player;
        private List<Room> rooms;
        private int currentRoomIndex;

        public Game()
        {
            string name;
            do
            {
                Console.Write("Enter your name: ");
                name = Console.ReadLine()?.Trim();
            } while (string.IsNullOrEmpty(name));

            player = new Player(name, 100);

            rooms = new List<Room>
            {
                new Room("Underground Fight Club", "A dark, cold room.", new Weapon("Boxing Gloves", 15), new Enemy("Goblin", 30, 5, "Boxing Gloves")),
                new Room("Spiritual Room", "A calm, serene space.", new Weapon("Elephant Tusk", 20), new Enemy("Dragon", 50, 10, "Elephant Tusk")),
                new Room("Vampire Den", "It reeks of blood and garlic.", new Weapon("Torch", 25), new Enemy("Vampire", 60, 12, "Torch")),
                new Room("Fortress of Solitude", "Chilly and glowing green.", new Weapon("Kryptonite", 999), new Enemy("Superman", 1000, 100, "Kryptonite"))
            };

            currentRoomIndex = 0;
            ShowRoomDetails();
        }

        private void ShowRoomDetails()
        {
            try
            {
                Console.WriteLine($"\n{rooms[currentRoomIndex].Name}: {rooms[currentRoomIndex].GetDescription()}");
                string item = rooms[currentRoomIndex].GetItem()?.Name;
                if (!string.IsNullOrEmpty(item))
                {
                    Console.WriteLine("You see a " + item);
                }

                Enemy enemy = rooms[currentRoomIndex].GetEnemy();
                if (enemy != null)
                {
                    Console.WriteLine($"You see a dangerous {enemy.Name}!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error displaying room: " + ex.Message);
            }
        }

        public void Start()
        {
            bool playing = true;
            while (playing)
            {
                Console.Write("\nWhat do you want to do? (move/pickup/status/fight/quit): ");
                string command = Console.ReadLine()?.Trim().ToLower();

                switch (command)
                {
                    case "pickup":
                        TryPickUpItem();
                        break;
                    case "move":
                        MoveToNextRoom();
                        break;
                    case "status":
                        player.ShowStatus();
                        break;
                    case "fight":
                        TryFightEnemy();
                        break;
                    case "quit":
                        Console.WriteLine("Goodbye!");
                        playing = false;
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }

        private void TryPickUpItem()
        {
            try
            {
                Weapon item = rooms[currentRoomIndex].Item;
                if (item != null)
                {
                    player.PickUpItem(item);
                    rooms[currentRoomIndex].RemoveItem();
                }
                else
                {
                    Console.WriteLine("There is nothing here to pick up.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }
        }

        private void MoveToNextRoom()
        {
            if (currentRoomIndex < rooms.Count - 1)
            {
                currentRoomIndex++;
                Console.WriteLine("You move to the next room...");
                ShowRoomDetails();
            }
            else
            {
                Console.WriteLine("There are no more rooms ahead.");
            }
        }

        private void TryFightEnemy()
        {
            Enemy enemy = rooms[currentRoomIndex].GetEnemy();
            if (enemy == null)
            {
                Console.WriteLine("There is no enemy here to fight.");
                return;
            }

            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("You have no weapons! Pick something up first.");
                return;
            }

            Console.WriteLine("Choose a weapon to attack with:");
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.Inventory[i].Name} (Damage: {player.Inventory[i].BaseDamage})");
            }

            int choice;
            while (true)
            {
                Console.Write("Enter weapon number: ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= player.Inventory.Count)
                    break;
                Console.WriteLine("Invalid choice. Try again.");
            }

            Weapon selectedWeapon = player.Inventory[choice - 1];
            player.AttackEnemy(enemy, selectedWeapon);

            if (enemy.Health <= 0)
            {
                Console.WriteLine($"You defeated the {enemy.Name}!");
                rooms[currentRoomIndex].Enemy = null;
            }
            else
            {
                Console.WriteLine($"{enemy.Name} has {enemy.Health} HP left.");
            }
        }
    }
}
