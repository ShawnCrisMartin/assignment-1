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
                new Room("Pirate Room", "Room full of gold", new Weapon("Kryptonite", 15), new Enemy("unknown country army", 10, 10, 10, "misile")),
                new Room("Demon Hut", "The demon is sleeping", new Weapon("Demon Slaying Sword", 15), new Enemy("demon", 1, 1, 1, "Kryptonite")),
                new Room("Samurai Camp", "A peaceful place", new Weapon("Samurai Sword", 5), new Enemy("scientist", 10, 10, 10, "chemical bomb")),
                new Room("Ninja Hideout", "Shadow filled", new Weapon("Nunchucks", 5), new Enemy("Ninjas", 30, 5, 5, "Samurai Sword")),
                new Room("Electronics Shop", "Busy marketplace", new Weapon("Torch", 5), new Enemy("shopkeeper", 10, 5, 5, "Kryptonite")),
                new Room("Spiritual Room", "A calm place", new Weapon("Elephant Tusk", 20), new Enemy("Monk", 50, 10, 10, "Elephant Tusk")),
                new Room("Vampire Den", "Smelling of blood", new Weapon("Drugs", 5), new Enemy("Vampire", 60, 12, 12, "Torch")),
                new Room("Metropolis", "Home of Superman", new Weapon("boomerang", 5), new Enemy("Superman", 1000, 100, 100, "Kryptonite")),
                new Room("Demon Hut 2", "The demon is awake", new Weapon("misile", 5), new Enemy("Demon", 300, 500, 500, "Demon Slaying Sword")),
                new Room("Underground Prison", "Steel cage", new Weapon("chemical bomb", 5), new Enemy("Goons", 30, 5, 5, "Nunchucks")),
                new Room("Upstairs Hideout", "Maximum security", new Weapon("gun", 5), new Enemy("Dealers", 30, 5, 5, "Drugs"))
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

            while (enemy.Health > 0 && player.Health > 0)
            {
                Console.WriteLine("\nChoose a weapon to attack with:");
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {player.Inventory[i].Name} (Damage: {player.Inventory[i].BaseDamage})");
                }

                int choice;
                while (true)
                {
                    Console.Write("Enter weapon number (or 0 to flee): ");
                    if (int.TryParse(Console.ReadLine(), out choice) && choice >= 0 && choice <= player.Inventory.Count)
                        break;
                    Console.WriteLine("Invalid choice. Try again.");
                }

                if (choice == 0)
                {
                    Console.WriteLine("You fled from the battle!");
                    return;
                }

                Weapon selectedWeapon = player.Inventory[choice - 1];
                player.AttackEnemy(enemy, selectedWeapon);

                if (enemy.Health <= 0)
                {
                    Console.WriteLine($"You defeated the {enemy.Name}!");
                    rooms[currentRoomIndex].Enemy = null;
                    break;
                }

                if (player.Health <= 0)
                {
                    Console.WriteLine("Game over! You were defeated.");
                    return;
                }

                // Show status after each round
                Console.WriteLine($"\nAfter battle:");
                Console.WriteLine($"{player.Name}: {player.Health} HP");
                Console.WriteLine($"{enemy.Name}: {enemy.Health} HP");
            }
        }
    }
}