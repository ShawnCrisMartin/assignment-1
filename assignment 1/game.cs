using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    class Game
    {
        private Player player;
        private List<Room> rooms;
        private int currentRoomIndex;

        public Game()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            player = new Player(name, 100);

            // creatig list of items with room and object in it
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

            currentRoomIndex = 0; // assigning 0 so that reading starts from first room
            ShowRoomDetails();
        }

        private void ShowRoomDetails()
        {
            Console.WriteLine(rooms[currentRoomIndex].GetDescription());
            if (rooms[currentRoomIndex].GetItem() != null)
            {
                Console.WriteLine("You see a " + rooms[currentRoomIndex].GetItem());
            }
        }

        public void Start()
        {
            bool playing = true;

            while (playing)
            {

                Console.Write("\nWhat do you want to do? (move/pickup/status/quit): ");
                string command = Console.ReadLine().ToLower();

                if (command == "pickup")
                {
                    if (rooms[currentRoomIndex].GetItem() != null)
                    {
                        player.PickUpItem(rooms[currentRoomIndex].GetItem());
                        rooms[currentRoomIndex].RemoveItem();
                    }
                    else
                    {
                        Console.WriteLine("there is nothing there to pick up");
                    }
                }
                else if (command == "move")
                {
                    MoveToNextRoom();
                }
                else if (command == "status")
                {
                    player.ShowStatus();
                }
                else if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    playing = false;
                }
                else
                {
                    Console.WriteLine("Invalid command.");
                }
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
                Console.WriteLine("There are no more rooms infront");
            }
        }
    }
