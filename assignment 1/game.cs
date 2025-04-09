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
            new Room("You are in an underground fight club", "A dark, cold room.", "boxing gloves", new Enemy("Goblin", 30, 5)),
            new Room("You are in a spiritual room", "A calm, serene space.", "elephant tusk", new Enemy("Dragon", 50, 10)),
            new Room("You are in a creepy dungeon", "It smells damp and musty.", "large egg", new Enemy("Zombie", 40, 8))
            // Add more rooms as needed...
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

        private void TryFightEnemy()
        {
            Enemy enemy = rooms[currentRoomIndex].GetEnemy();
            if (enemy != null)
            {
                player.AttackEnemy(enemy);
                if (enemy.Health <= 0)
                {
                    Console.WriteLine("You defeated the enemy!");
                    rooms[currentRoomIndex].Enemy = null; // Remove enemy after defeat
                }
            }
            else
            {
                Console.WriteLine("There is no enemy here to fight.");
            }
        }
    }
}
