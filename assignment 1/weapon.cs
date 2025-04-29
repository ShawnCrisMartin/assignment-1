using DungeonExplorer;
using System;

public class Weapon : ICollectable
{
    public string Name { get; private set; }
    public int BaseDamage { get; private set; }

    public Weapon(string name, int damage)
    {
        Name = name;
        BaseDamage = damage;
    }

    public void OnCollect(Player player)
    {
        player.PickUpItem(this);
        Console.WriteLine(Name + " added to inventory.");
    }
}
