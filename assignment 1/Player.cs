class Player

    // sotring players name and health
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    private List<string> inventory = new List<string>(); // List to store collected items

    //defining the variable name and health
    public Player(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public void PickUpItem(string item)
    {
        try
        {
            // Adding item to the player's inventory
            inventory.Add(item);
            Console.WriteLine("You picked up: " + item);
        }
        catch (Exception ex)

        // Error handling
        {
            Console.WriteLine("Error picking up item: " + ex.Message);
        }
    }

    public void ShowStatus()
    {
        // Displaying player details, including inventory
        Console.WriteLine($"\nPlayer: {Name}");
        Console.WriteLine($"Health: {Health}");
        Console.WriteLine("Inventory: " + (inventory.Count > 0 ? string.Join(", ", inventory) : "Empty"));
    }
}