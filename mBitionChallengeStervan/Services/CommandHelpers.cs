using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mBitionChallengeStervan.services;

namespace mBitionChallengeStervan.services
{
    public static class CommandHelper
    {
        public static string ReadCommand()
        {
            Console.Write("Enter a command: ");
            string? input = Console.ReadLine();
            return input?.Trim().ToLower() ?? string.Empty;
        }

        public static void HandleCommand(string command, EventManager eventManager)
        {
            string[] commandParts = command.Split(' ');
            switch (commandParts[0])
            {
                case "create":
                    eventManager.CreateEvent();
                    break;
                case "list":
                    eventManager.ListEvents();
                    break;
                case "get":
                    if (TryGetId(commandParts, out int getId)) eventManager.GetEvent(getId);
                    break;
                case "delete":
                    if (TryGetId(commandParts, out int deleteId)) eventManager.DeleteEvent(deleteId);
                    break;
                case "update":
                    if (TryGetId(commandParts, out int updateId)) eventManager.UpdateEvent(updateId);
                    break;
                default:
                    Console.WriteLine("\u001b[31mUnknown command. Try again.\u001b[0m");
                    break;
            }
        }


        private static bool TryGetId(string[] commandParts, out int id)
        {
            id = -1;
            if (commandParts.Length == 2 && int.TryParse(commandParts[1], out id))
            {
                return true;
            }
            Console.WriteLine("Invalid or missing ID. Please provide a valid number.");
            return false;
        }
    }
}
