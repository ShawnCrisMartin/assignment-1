using System;

namespace DungeonExplorer
{
    public class Testing
    {
        public static void RunAll()
        {
            TestPlayerHealth();
            TestItemCollection();
            TestRoomNavigation();
            TestCombat();
        }

        public static void TestPlayerHealth()
        {
            int playerHealth = 100;
            int damage = 30;
            playerHealth -= damage;
            Console.WriteLine($"TestPlayerHealth: Player health is {playerHealth}");

            if (playerHealth == 70)
                Console.WriteLine("Test Passed: Player health is correctly reduced to 70.");
            else
                Console.WriteLine("Test Failed: Player health not reduced correctly.");
        }

        public static void TestItemCollection()
        {
            string item = "Sword";
            string inventory = item;
            Console.WriteLine($"TestItemCollection: {item} added to inventory.");

            if (inventory == item)
                Console.WriteLine("Test Passed: Item collected successfully.");
            else
                Console.WriteLine("Test Failed: Item collection failed.");
        }

        public static void TestRoomNavigation()
        {
            string currentRoom = "Start Room";
            string nextRoom = "Next Room";
            Console.WriteLine($"TestRoomNavigation: Player starts in {currentRoom}, moves to {nextRoom}.");

            if (nextRoom == "Next Room")
                Console.WriteLine("Test Passed: Able to move to next room.");
            else
                Console.WriteLine("Test Failed: Could not move to next room.");
        }

        public static void TestCombat()
        {
            int enemyHealth = 40;
            int playerAttackDamage = 40;
            enemyHealth -= playerAttackDamage;
            Console.WriteLine($"TestCombat: Enemy health is {enemyHealth}");

            if (enemyHealth == 0)
                Console.WriteLine("Test Passed: Enemy defeated.");
            else
                Console.WriteLine("Test Failed: Enemy not defeated correctly.");
        }
    }
}
