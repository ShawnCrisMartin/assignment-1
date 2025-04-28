using System;
using DungeonExplorer;

namespace DungeonExplorer

{
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollectable Item { get; private set; }
        public Enemy Enemy { get; set; }

        public Room(string name, string description, ICollectable item, Enemy enemy)
        {
            Name = name;
            Description = description;
            Item = item;
            Enemy = enemy;
        }

        public ICollectable GetItem()
        {
            return Item;
        }

        public string GetDescription()
        {
            return Description;
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