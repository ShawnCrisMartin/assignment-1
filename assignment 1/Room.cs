using System;

namespace DungeonExplorer
{
    public class Room
    {
        private string description;
        private string item;
        private Enemy enemy;

        public Room(string description, string item)
        {
            this.description = description;
            this.item = item;
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetItem()
        {
            return item;
        }

        public Enemy GetEnemy()  
        {
            return enemy;
        }

        public void RemoveItem()
        {
            item = null;
        }
    }
}
