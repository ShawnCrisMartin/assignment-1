using System;
using DungeonExplorer;

namespace DungeonExplorer

{
    // this class is the rooms class that defines each room in the dungeon, each room can have a name, a description, an item, and an enemy.
    public class Room


    {

        // the name of the room, and description of the room
        public string Name { get; set; }
        public string Description { get; set; }

        // the items the player an collect, defined in the icollectable interface
        public ICollectable Item { get; private set; }

        // the enemy the player has to fight 
        public Enemy Enemy { get; set; }

        // constructor for the Room class.setting up the room with a name, description, optional item, and enemy.
        public Room(string name, string description, ICollectable item, Enemy enemy)
        {
            Name = name;
            Description = description;
            Item = item;
            Enemy = enemy;
        }

        // allows the player to see what collectible item is in the room
        public ICollectable GetItem()
        {
            return Item;
        }

        // return the description of the room as the function getdescription
        public string GetDescription()
        {
            return Description;
        }


        // return the enemy of the room as the function getenemy, 
        public Enemy GetEnemy()
        {
            return Enemy;
        }

        // Removes the item from the room, so that ones the player pickus up something it is not there anymore
        public void RemoveItem()
        {
            Item = null;
        }
    }
}