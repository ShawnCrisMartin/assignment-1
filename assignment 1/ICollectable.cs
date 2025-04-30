using System;
namespace DungeonExplorer
{
    // This interface for any item in the game that can be collected by the player.

    public interface ICollectable
    {
        // anything that the player collect should have the property name.
        string Name { get; }
        // it should also have this oncollect method, and it is called when a player picks up the item, and it defines what happens when collected.
        void OnCollect(Player player);
    }
}
