using mBitionChallengeStervan.database;
using mBitionChallengeStervan.services;
using mBitionChallengeStervan.Utils;

namespace MBitionChallenge
{
    class Program
    {
        static void Main()
        {
            DatabaseHelper.InitializeDatabase();
            EventManager eventManager = new EventManager();

            DisplayHelper.DisplayWelcomeMessage();

            string command;
            do
            {
                command = CommandHelper.ReadCommand();
                if (command != "exit")
                {
                    CommandHelper.HandleCommand(command, eventManager);
                }
            } while (command != "exit");
        }


    }
}
