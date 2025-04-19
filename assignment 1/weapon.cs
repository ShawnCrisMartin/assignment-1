using System;

namespace DungeonExplorer
{
    public class Weapon
    {
        public string Name { get; set; }
        public int BaseDamage { get; set; }

        public Weapon(string name, int baseDamage)
        {
            Name = name;
            BaseDamage = baseDamage;
        }
    }
}