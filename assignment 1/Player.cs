public class Player
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public List<string> Inventory { get; set; }

    public Player(string name, int health)
    {
        Name = name;
        Health = health;
        Attack = 10; // Default attack value
        Inventory = new List<string>();
    }

    public void ShowStatus()
    {
        Console.WriteLine($"{Name} - Health: {Health}, Attack: {Attack}");
        Console.WriteLine("Inventory:");
        foreach (var item in Inventory)
        {
            Console.WriteLine($"- {item}");
        }
    }

    public void PickUpItem(string item)
    {
        Inventory.Add(item);
        Console.WriteLine($"Picked up: {item}");
    }

    public void AttackEnemy(Enemy enemy)
    {
        Console.WriteLine($"{Name} attacks {enemy.Name} for {Attack} damage.");
        enemy.TakeDamage(Attack);
    }
}
