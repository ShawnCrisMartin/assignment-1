namespace DungeonExplorer
{
    public abstract class Characters
    {
        public string CharacterName { get; set; }
        public int CharacterHealth { get; set; }

        public Characters(string name, int health)
        {
            CharacterName = name;
            CharacterHealth = health;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Name: {CharacterName}");
            Console.WriteLine($"Health: {CharacterHealth}");
        }
    }
}
