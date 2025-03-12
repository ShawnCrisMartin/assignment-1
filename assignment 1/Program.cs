using System;
using System.Collections.Generic;
using System.Numerics;

namespace DungeonExplorer
{
    class Game
    {
        private Player player;
        private List<Room> rooms;
        private int currentRoomIndex;

        public Game()
        {
            Console.Write("enter your name: ");
            string name = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(name))
                name = "player1"; // to prevent any empty input

            player = new Player(name, 100);

            // Room list with all the descriptions and items
            rooms = new List<Room>
            {
                new Room("You are in a underground fight club", "boxing gloves"),
                new Room("You are in a spiritual room", "elephant tusk"),
                new Room("You are in a creepy dungeon", "large egg"),
                new Room("You are in a underground cave", "treasure box"),
                new Room("You entering a scary room", "ancient scribe"),
                new Room("You are in a creepy dungeon", "block of ice"),
                new Room("You are in a hunters den", "tiger skin"),
                new Room("You are in a ghost assylum", "wet towel"),
                new Room("You entering wepons locker", "dagger"),
                new Room("You just opened gun collection", "M416"),
                new Room("You entering wepons locker", "Glock19")
            };

            currentRoomIndex = 0;
            ShowRoomDetails();
        }

        private void ShowRoomDetails()
        {
            try
            {
                Console.WriteLine(rooms[currentRoomIndex].GetDescription());
                string item = rooms[currentRoomIndex].GetItem();
                if (!string.IsNullOrEmpty(item))
                {
                    Console.WriteLine("You see a " + item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("sorry error displaying room: " + ex.Message);
            }
        }

        public void Start()
        {
            bool playing = true;

            while (playing)
            {
                Console.Write("What do you want to do? (move/pickup/status/quit): ");
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
                    case "quit":
                        Console.WriteLine("goodbye!");
                        playing = false;
                        break;
                    default:
                        Console.WriteLine("sorry, Invalid command.");
                        break;
                }
            }
        }

        private void TryPickUpItem()
        {
            try
            {
                string item = rooms[currentRoomIndex].GetItem();
                if (!string.IsNullOrEmpty(item))
                {
                    player.PickUpItem(item);
                    rooms[currentRoomIndex].RemoveItem();
                }
                else
                {
                    Console.WriteLine("there is nothing there to pick up");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("unexpected error: " + ex.Message);
            }
        }

        private void MoveToNextRoom()
        {
            if (currentRoomIndex < rooms.Count - 1)
            {
                currentRoomIndex++;
                Console.WriteLine("you move to the next room...");
                ShowRoomDetails();
            }
            else
            {
                Console.WriteLine("there are no more rooms in front.");
            }
        }
    }

    class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();

        public Player(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public void PickUpItem(string item)
        {
            try
            {
                inventory.Add(item);
                Console.WriteLine("you picked up: " + item);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error picking up item: " + ex.Message);
            }
        }

        public void ShowStatus()
        {
            Console.WriteLine($"\nPlayer: {Name}");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine("Inventory: " + (inventory.Count > 0 ? string.Join(", ", inventory) : "Empty"));
        }
    }

    class Room
    {
        private string description;
        private string item;

        public Room(string description, string item)
        {
            this.description = description;
            this.item = item;
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetItem()
        {
            return item;
        }

        public void RemoveItem()
        {
            item = null;
        }
    }

    class Program
    {
        static void Main()
        {
            Game game = new Game();
            game.Start();
        }
    }
}
