using DungeonExplorer;

public abstract class Character : IDamage
{
    public string Name { get; set; }
    public int Health { get; set; }

    // Constructor to initialize Name and Health
    public Character(string name, int health)
    {
        Name = name;
        Health = health;
    }

    // This method will be used to apply damage to the character
    public virtual void DamageTaken(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }
}
