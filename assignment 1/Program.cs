namespace DungeonExplorer
{

    class Program
    {
        static void Main()
        {
            try
            {
                Game game = new Game();
                game.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
