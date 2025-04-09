public class Room
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Item { get; set; }
    public Enemy Enemy { get; set; }

    public Room(string name, string description, string item = null, Enemy enemy = null)
    {
        Name = name;
        Description = description;
        Item = item;
        Enemy = enemy;
    }

    public string GetDescription()
    {
        return Description;
    }

    public string GetItem()
    {
        return Item;
    }

    public Enemy GetEnemy()
    {
        return Enemy;
    }

    public void RemoveItem()
    {
        Item = null;
    }
}
