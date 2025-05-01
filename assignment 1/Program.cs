using DungeonExplorer;

namespace DungeonExplorer
{
    //entry point to the whole game
    class Program
    {
        static void Main()
        {
            try
            {

                //running the testing class
                Testing.RunAll();
                //new instance of the game
                Game game = new Game();

                //this is used to start the game
                game.Start();
            }
            catch (Exception ex)
            {

                // error handling, when something unexpected happens this catches it and display a message.
                Console.WriteLine("error: " + ex.Message);
            }
        }
    }
}