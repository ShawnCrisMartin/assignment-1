using System;
using DungeonExplorer;

namespace DungeonExplorer
{
    // running the main logic here, this class
    public class Game
    {
        private Player player;         // using to hold the data of the player name,  health, inventory
        private GameMap map;           // all the rooms, with weapons to pick and the enemies
        private int currentRoomIndex;  // traking the room player is currently as he moves

        // initializing the game
        public Game()
        {
            // asking player for name, and also looping till the input is not null
            string name;
            do
            {
                Console.Write("Enter your name: ");
                name = Console.ReadLine()?.Trim();
            } while (string.IsNullOrEmpty(name));  

            // players hp set to 100 points
            player = new Player(name, 100);

            // rooms called in
            map = new GameMap();

            // Starting from the first room
            currentRoomIndex = 0;

            //to display the description
            ShowRoomDetails();
        }

        //descrpition of the room
        private void ShowRoomDetails()
        {
            var room = map.GetRoom(currentRoomIndex);

            if (room == null)
            {
                Console.WriteLine("No room here."); //message when room is null
                return;
            }

            Console.WriteLine($"\n{room.Name}: {room.GetDescription()}");

            var item = room.GetItem();
            if (item != null)
                Console.WriteLine("You see a " + item.Name); //item being displayed when item value is not empty

            var enemy = room.GetEnemy();
            if (enemy != null)
                Console.WriteLine($"You see a dangerous {enemy.Name}!"); //enemy being displayed when enemy value is not empty
        }

        // Starts the game loop where the player chooses actions until they quit or die
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
                        TryPickUpItem(); break;

                    case "move":
                        MoveToNextRoom(); break;

                    case "status":
                        player.ShowStatus(); break;

                    case "fight":
                        TryFightEnemy(); break;

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

        // tries to pick up an item from the current room and add it to player's inventory
        private void TryPickUpItem()
        {
            var room = map.GetRoom(currentRoomIndex);
            var item = room.Item;

            if (item != null)
            {
                item.OnCollect(player);  // items collection logicc from icollectable interface
                room.RemoveItem();       // removing it from the room, so that no dupliaction,
            }
            else
            {
                Console.WriteLine("There is nothing here to pick up."); //only when empty after already picking up
            }
        }

        // moving the player to the next room
        private void MoveToNextRoom()
        {
            if (currentRoomIndex < map.RoomCount - 1)
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

        // fighting with the enemy
        private void TryFightEnemy()
        {
            var room = map.GetRoom(currentRoomIndex);
            var enemy = room.GetEnemy();

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

            // Combat loop, turn based attacks between enemy and player.
            while (enemy.Health > 0 && player.Health > 0)
            {
                Console.WriteLine("\nChoose a weapon to use:");

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

                var selectedWeapon = player.Inventory[choice - 1];

                if (selectedWeapon.Name.Equals("Health Kit", StringComparison.OrdinalIgnoreCase))
                {
                    // Heals player if chosen weapon fo attack is a health kit
                    player.UseHealthKit();
                    Console.WriteLine($"{player.Name} used {selectedWeapon.Name} and healed to {player.Health} health.");
                }
                else
                {
                    // attacking the enemy
                    player.AttackEnemy(enemy, selectedWeapon);
                }

                // logic when enemy is defeated
                if (enemy.Health <= 0)
                {
                    Console.WriteLine($"You defeated the {enemy.Name}!");
                    room.Enemy = null;
                    break;
                }

                // enemy attacking back
                Console.WriteLine($"\n{enemy.Name} attacks you!");
                player.DamageTaken(enemy.Damage);

                if (player.Health <= 0)
                {
                    Console.WriteLine("Game over! You were defeated.");
                    return;
                }

                // after each round of battle, between the enemy and the player, both the players and enemies health is displayed so that the player can decide if they want to use the health kit to heal or not.
                Console.WriteLine($"\nAfter battle:");
                Console.WriteLine($"{player.Name}: {player.Health} HP");
                Console.WriteLine($"{enemy.Name}: {enemy.Health} HP");
            }
        }
    }
}
