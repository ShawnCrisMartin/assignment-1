using System;

namespace DungeonExplorer
{
    // Base abstract class for all items in the game.
    public abstract class Item
    {
        public string Name { get; private set; }

        public Item(string name)
        {
            Name = name;
        }

        // Abstract method that must be implemented by all subclasses.
        public abstract void OnCollect(Player player);
    }
}
