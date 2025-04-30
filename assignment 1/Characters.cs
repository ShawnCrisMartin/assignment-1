using DungeonExplorer;

//abstract class, so have to create enemy and player from this, because annot use it directly
public abstract class Character : IDamage
{
    public string Name { get; set; }
    public int Health { get; set; }

    // cconstructor to initialize name and health
    public Character(string name, int health)
    {
        Name = name;
        Health = health;
    }

    // this method will be used to apply damage to the character, and it is set as virtual so that the player and enemy calsses if wants to can overide it.
    public virtual void DamageTaken(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }
}