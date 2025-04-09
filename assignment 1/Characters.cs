public abstract class Character
{
    // Made protected so that derived classes can access it
    public string Name { get; set; }
    protected int Health { get; set; }

    // Constructor to initialize Name and Health
    public Character(string name, int health)
    {
        Name = name;
        Health = health;
    }
}
