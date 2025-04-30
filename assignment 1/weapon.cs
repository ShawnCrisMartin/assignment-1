using DungeonExplorer;
using System;

// this class represents a weapon in the game for the player to pick up and use to attack enemies.
public class Weapon : ICollectable
{
    // name of the weapon, and the damage the attack witht he particular weapon has, usually, not when it is a weakness weapon
    public string Name { get; private set; }
    public int BaseDamage { get; private set; }

    // constructor to create a new weapon and nees a name and damage value.
    public Weapon(string name, int damage)
    {
        Name = name;
        BaseDamage = damage;
    }

    // this method is called when a player collects this weapon, add the weapon to the inventory and displays for the user to pick from while attacking an enemy
    public void OnCollect(Player player)
    {
        player.PickUpItem(this);  // Adds the weapon to the player's inventory list.
        Console.WriteLine(Name + " added to inventory.");  // Confirms to the player that the weapon was picked up.
    }
}
