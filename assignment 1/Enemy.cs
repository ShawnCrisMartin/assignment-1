// Rename this class from Monster to Enemy
using DungeonExplorer;

public class Enemy
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int AttackPower { get; private set; }

    public Enemy(string name, int health, int attackPower)
    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }

    public void Attack(Player player)
    {
        player.TakeDamage(AttackPower);
        Console.WriteLine($"{Name} attacks {player.Name} for {AttackPower} damage!");
    }
}
