using System;
namespace DungeonExplorer
{
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Weapon Item { get; private set; }
        public Enemy Enemy { get; set; }

        public Room(string name, string description, Weapon item, Enemy enemy)
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

        public Weapon GetItem()
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
}