namespace DungeonExplorer
{
    public class Player
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
                Console.WriteLine("You picked up: " + item);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error picking up item: " + ex.Message);
            }
        }

        public void ShowStatus()
        {
            Console.WriteLine($"\nPlayer: {Name}");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine("Inventory: " + (inventory.Count > 0 ? string.Join(", ", inventory) : "Empty"));
        }
    }
}

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
                new Room("You are in an underground fight club", "boxing gloves"),
                new Room("You are in a spiritual room", "elephant tusk"),
                new Room("You are in a creepy dungeon", "large egg"),
                new Room("You are in an underground cave", "treasure box"),
                new Room("You are entering a scary room", "ancient scribe"),
                new Room("You are in a creepy dungeon", "block of ice"),
                new Room("You are in a hunter's den", "tiger skin"),
                new Room("You are in a ghost asylum", "wet towel"),
                new Room("You are entering a weapons locker", "dagger"),
                new Room("You just opened a gun collection", "M416"),
                new Room("You are entering a weapons locker", "Glock19")
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
                Console.WriteLine("Error displaying room: " + ex.Message);
            }
        }

        public void Start()
        {
            bool playing = true;
            while (playing)
            {
                Console.Write("\nWhat do you want to do? (move/pickup/status/quit): ");
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
                string item = rooms[currentRoomIndex].GetItem();
                if (!string.IsNullOrEmpty(item))
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
    }
}

class Program
{
    static void Main()
    {
        try
        {
            Game game = new Game();
            game.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}

namespace DungeonExplorer
{
    public class Room
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
            item = string.Empty; // Use an empty string instead of null
        }
    }
}
