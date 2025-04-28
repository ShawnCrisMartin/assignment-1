namespace DungeonExplorer
{
    public interface ICollectable
    {
        string Name { get; }
        void Collect(Player player);
    }
}
