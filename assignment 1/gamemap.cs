using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class GameMap
    {
        private List<Room> rooms;

        public GameMap()
        {
            rooms = new List<Room>
            {
                new Room("Pirate Room", "Room full of gold", new Weapon("Kryptonite", 15), new Enemy("unknown country army", 10, 10, 10, "missile")),
                new Room("Demon Hut", "The demon is sleeping", new Weapon("Demon Slaying Sword", 15), new Enemy("demon", 1, 1, 1, "Kryptonite")),
                new Room("Samurai Camp", "A peaceful place", new Weapon("Samurai Sword", 5), new Enemy("scientist", 10, 10, 10, "chemical bomb")),
                new Room("Health Kit Room", "Room with health kit", new Weapon("Health Kit", 0), new Enemy("Zombie", 30, 10, 5, "gun")),
                new Room("Ninja Hideout", "Shadow filled", new Weapon("Nunchucks", 5), new Enemy("Ninjas", 30, 5, 5, "Samurai Sword")),
                new Room("Electronics Shop", "Busy marketplace", new Weapon("Torch", 5), new Enemy("shopkeeper", 10, 5, 5, "Kryptonite")),
                new Room("Spiritual Room", "A calm place", new Weapon("Elephant Tusk", 20), new Enemy("Monk", 50, 10, 10, "Elephant Tusk")),
                new Room("Vampire Den", "Smelling of blood", new Weapon("Drugs", 5), new Enemy("Vampire", 60, 12, 12, "Torch")),
                new Room("Metropolis", "Home of Superman", new Weapon("Boomerang", 5), new Enemy("Superman", 1000, 100, 100, "Kryptonite")),
                new Room("Demon Hut 2", "The demon is awake", new Weapon("Missile", 5), new Enemy("Demon", 300, 500, 500, "Demon Slaying Sword")),
                new Room("Underground Prison", "Steel cage", new Weapon("Chemical Bomb", 5), new Enemy("Goons", 30, 5, 5, "Nunchucks")),
                new Room("Upstairs Hideout", "Maximum security", new Weapon("Gun", 5), new Enemy("Dealers", 30, 5, 5, "Drugs"))
            };
        }

        public Room GetRoom(int index)
        {
            if (index >= 0 && index < rooms.Count)
                return rooms[index];
            else
                return null;
        }

        public int RoomCount => rooms.Count;
    }
}
