using System;
using DungeonExplorer;
using System.Linq;

namespace DungeonExplorer
{
    public class Game
    {
        private Player player;
        private GameMap map;
        private int currentRoomIndex;

        // constructor to initialize the game and set up the player and map
        public Game()
        {
            string name;
            do
            {
                Console.Write("Enter your name: ");
                name = Console.ReadLine()?.Trim();
            } while (string.IsNullOrEmpty(name));

            // Initialize player with name and 100 health
            player = new Player(name, 100); 
            //Initialize the game map
            map = new GameMap();  
            currentRoomIndex = 0;  
            ShowRoomDetails();  
        }

        // method to display the details of the current room
        private void ShowRoomDetails()
        {
            var room = map.GetRoom(currentRoomIndex);

            // if no room is there then displaying message pf it
            if (room == null)
            {
                Console.WriteLine("No room here.");
                return;
            }

            // show the room's name and description
            Console.WriteLine("\n" + room.Name + ": " + room.GetDescription());

            // show item in the room if it exists
            var item = room.GetItem();
            if (item != null)
                Console.WriteLine("You see a " + item.Name);

            // show enemy in the room if it exists
            var enemy = room.GetEnemy();
            if (enemy != null)
                Console.WriteLine("You see a dangerous " + enemy.Name + "!");
        }



        // main game loop to start the game and process player commands
        public void Start()
        {
            bool playing = true;

            while (playing)
            {
                // asking for command
                Console.Write("\nWhat do you want to do? (move/pickup/status/fight/quit): ");
                string command = Console.ReadLine()?.Trim().ToLower();

                // Handle different commands
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

        // method to attempt picking up an item from the current room
        private void TryPickUpItem()
        {
            var room = map.GetRoom(currentRoomIndex);
            var item = room.Item;

            // Lambda expression to check if item exists and then pick up
            Action pickUpItem = item != null ? new Action(() =>
            {
                item.OnCollect(player);
                room.RemoveItem();
            }) : new Action(() => Console.WriteLine("There is nothing here to pick up."));

            pickUpItem.Invoke();
        }

        // method to move to the next room in the game
        private void MoveToNextRoom()
        {
            if (currentRoomIndex < map.RoomCount - 1)
            {
                currentRoomIndex++;  // add 1 in room index to move to the next room
                Console.WriteLine("You move to the next room...");
                ShowRoomDetails();  // shows the details of the new room
            }
            else
            {
                Console.WriteLine("There are no more rooms ahead.");  // No more rooms to move to
            }
        }


        // method to attempt fighting an enemy in the current room
        private void TryFightEnemy()
        {
            var room = map.GetRoom(currentRoomIndex);
            var enemy = room.GetEnemy();

            // when no enemy is present, informing the player
            if (enemy == null)
            {
                Console.WriteLine("There is no enemy here to fight.");
                return;
            }

            // if the player has no weapons, they cannot fight
            if (player.Inventory.Count == 0)
            {
                Console.WriteLine("You have no weapons! Pick something up first.");
                return;
            }

            // Battle loop, continues as long as both the player and enemy are alive,so.. turn based
            while (enemy.Health > 0 && player.Health > 0)
            {
                Console.WriteLine("\nChoose a weapon to use:");

                // display available weapons to the player
                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + player.Inventory[i].Name + " (Damage: " + player.Inventory[i].BaseDamage + ")");
                }

                int choice;
                while (true)
                {
                    // get the player's choice of weapon
                    Console.Write("Enter weapon number (or 0 to flee): ");
                    if (int.TryParse(Console.ReadLine(), out choice) && choice >= 0 && choice <= player.Inventory.Count)
                        break;

                    Console.WriteLine("Invalid choice. Try again.");  // Ensure valid weapon choice
                }

                // if the player chooses to flee by entering number 0, exit the fight
                if (choice == 0)
                {
                    Console.WriteLine("You fled from the battle!");
                    return;
                }

                var selectedWeapon = player.Inventory[choice - 1];

                // if the player uses a health kit, heal instead of attacking
                if (selectedWeapon.Name.Equals("Health Kit", StringComparison.OrdinalIgnoreCase))
                {
                    player.UseHealthKit();
                    Console.WriteLine(player.Name + " used " + selectedWeapon.Name + " and healed to " + player.Health + " health.");
                }
                else
                {
                    // if not then attack the enemy
                    player.AttackEnemy(enemy, selectedWeapon);
                }

                // if the enemy is defeated, end the battle
                if (enemy.Health <= 0)
                {
                    Console.WriteLine("You defeated the " + enemy.Name + "!");
                    room.Enemy = null;  // Remove enemy from the room, once dead only
                    break;
                }

                // if the enemy is still alive, it attacks the player
                Console.WriteLine("\n" + enemy.Name + " attacks you!");
                player.DamageTaken(enemy.Damage);

                // if the player dies, end the game, and goodbye message is displayed
                if (player.Health <= 0)
                {
                    Console.WriteLine("you are dead....bye");
                    return;
                }

                // displays the health status after the battle each turn, as the fiht continues
                Console.WriteLine("\nafter battle:");
                Console.WriteLine(player.Name + ": " + player.Health + " HP");
                Console.WriteLine(enemy.Name + ": " + enemy.Health + " HP");
            }
        }
    }
}
