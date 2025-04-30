using System;
using DungeonExplorer;

namespace DungeonExplorer
{
    public class Game
    {
        private Player player;
        private GameMap map;
        private int currentRoomIndex;

        public Game()
        {
            string name;
            do
            {
                Console.Write("enter your name: ");
                name = Console.ReadLine()?.Trim();
            } while (string.IsNullOrEmpty(name));

            player = new Player(name, 100);
            map = new GameMap();
            currentRoomIndex = 0;
            ShowRoomDetails();
        }

        private void ShowRoomDetails()
        {
            var room = map.GetRoom(currentRoomIndex);

            if (room == null)
            {
                Console.WriteLine("no room here.");
                return;
            }

            Console.WriteLine("\n" + room.Name + "is this..." + room.GetDescription());

            var item = room.GetItem();
            if (item != null)
                Console.WriteLine("you see a " + item.Name);

            var enemy = room.GetEnemy();
            if (enemy != null)
                Console.WriteLine("you see a dangerous " + enemy.Name );
        }

        public void Start()
        {
            bool playing = true;

            while (playing)
            {
                Console.Write("\nwhat do you want to do? (move/pickup/status/fight/quit): ");
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
                        Console.WriteLine("Goodbye");
                        playing = false;
                        break;
                    default:
                        Console.WriteLine("invalid command.");
                        break;
                }
            }
        }

        private void TryPickUpItem()
        {
            var room = map.GetRoom(currentRoomIndex);
            var item = room.Item;

            if (item != null)
            {
                item.OnCollect(player);
                room.RemoveItem();
            }
            else
            {
                Console.WriteLine("there is nothing here to pick up.");
            }
        }

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

            while (enemy.Health > 0 && player.Health > 0)
            {
                Console.WriteLine("\nChoose a weapon to use:");

                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + player.Inventory[i].Name + " (Damage: " + player.Inventory[i].BaseDamage + ")");
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
                    player.UseHealthKit();
                    Console.WriteLine(player.Name + " used " + selectedWeapon.Name + " and healed to " + player.Health + " health.");
                }
                else
                {
                    player.AttackEnemy(enemy, selectedWeapon);
                }

                if (enemy.Health <= 0)
                {
                    Console.WriteLine("You defeated the " + enemy.Name + "!");
                    room.Enemy = null;
                    break;
                }

                Console.WriteLine("\n" + enemy.Name + " attacks you!");
                player.DamageTaken(enemy.Damage);

                if (player.Health <= 0)
                {
                    Console.WriteLine("you are dead....bye");
                    return;
                }

                Console.WriteLine("\nafter battle:");
                Console.WriteLine(player.Name + ": " + player.Health + " HP");
                Console.WriteLine(enemy.Name + ": " + enemy.Health + " HP");
            }
        }
    }
}
