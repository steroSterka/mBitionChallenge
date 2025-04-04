
namespace mBitionChallengeStervan.Utils
{
    public static class DisplayHelper
    {
        public static void DisplayWelcomeMessage()
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("\u001b[32m      WELCOME TO THE EVENT MANAGEMENT SYSTEM\u001b[0m");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("\nAvailable Commands: create | list | get <id> | delete <id> | update <id> | exit\n");
            Console.WriteLine(new string('=', 50));
        }
    }
}
