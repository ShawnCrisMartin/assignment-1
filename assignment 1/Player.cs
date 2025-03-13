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
}