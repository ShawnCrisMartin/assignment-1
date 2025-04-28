using System;
namespace DungeonExplorer
{
    public interface ICollectable
    {
        string Name { get; }
        void OnCollect(Player player);
    }
}
