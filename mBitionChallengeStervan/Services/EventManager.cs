using Microsoft.Data.Sqlite;
using mBitionChallengeStervan.database;


namespace mBitionChallengeStervan.services
{
    public interface IEventManager
    {
        void CreateEvent();
        void ListEvents();
        void GetEvent(int id);
        void DeleteEvent(int id);
        void UpdateEvent(int id);
    }

    public class EventManager : IEventManager
    {
        private string ReadInput(string prompt, bool required = true)
        {
            do
            {
                Console.Write(prompt);
                string? input = Console.ReadLine()?.Trim();
                if (!required || !string.IsNullOrWhiteSpace(input))
                {
                    return input ?? string.Empty;
                }
                Console.WriteLine("Input cannot be empty.");
            } while (true);
        }

        private DateTime ReadValidDate(string prompt)
        {
            DateTime date;
            do
            {
                Console.Write(prompt);
                string? input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty. Please enter a valid date.");
                    continue;
                }
                else if (DateTime.TryParse(input, out date) && date >= DateTime.Today)
                {
                    return date;
                }
                else
                {
                    Console.WriteLine("Invalid date. Must be in the future (yyyy-MM-dd). Try again.");
                }
                    
                
            } while (true);
        }

        private SqliteParameter CreateParameter(string name, object value) => new(name, value ?? DBNull.Value);

        public void CreateEvent()
        {
            string name = ReadInput("Enter Event Name: ");
            string description = ReadInput("Enter Description (optional): ", false);
            DateTime date = ReadValidDate("Enter Date (yyyy-MM-dd): ");
            string location = ReadInput("Enter Location (optional): ", false);

            try
            {
                using var connection = DatabaseHelper.GetConnection();

                const string query = "INSERT INTO Events (Name, Description, Date, Location) VALUES (@name, @description, @date, @location)";
                using var command = new SqliteCommand(query, connection);
                command.Parameters.Add(CreateParameter("@name", name));
                command.Parameters.Add(CreateParameter("@description", description));
                command.Parameters.Add(CreateParameter("@date", date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(CreateParameter("@location", location));
                command.ExecuteNonQuery();
                Console.WriteLine("Event created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ListEvents()
        {
            try
            {
                using var connection = DatabaseHelper.GetConnection();

                const string query = "SELECT * FROM Events";
                using var command = new SqliteCommand(query, connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Date: {reader["Date"]}, Location: {reader["Location"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching events: {ex.Message}");
            }
        }

        public void GetEvent(int id)
        {
            try
            {
                using var connection = DatabaseHelper.GetConnection();
                const string query = "SELECT * FROM Events WHERE Id = @id";
                using var command = new SqliteCommand(query, connection);
                command.Parameters.Add(CreateParameter("@id", id));
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Date: {reader["Date"]}, Location: {reader["Location"]}");
                }
                else
                {
                    Console.WriteLine("Event not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching event: {ex.Message}");
            }
        }

        public void DeleteEvent(int id)
        {
            try
            {
                using var connection = DatabaseHelper.GetConnection();

                const string query = "DELETE FROM Events WHERE Id = @id";
                using var command = new SqliteCommand(query, connection);
                command.Parameters.Add(CreateParameter("@id", id));
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0 ? "Event deleted successfully." : "Event not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting event: {ex.Message}");
            }
        }

        public void UpdateEvent(int id)
        {
            string name = ReadInput("Enter new Event Name: ");
            string description = ReadInput("Enter new Description (optional): ", false);
            DateTime date = ReadValidDate("Enter new Date (yyyy-MM-dd): ");
            string location = ReadInput("Enter new Location (optional): ", false);

            try
            {
                using var connection = DatabaseHelper.GetConnection();
       
                const string query = "UPDATE Events SET Name = @name, Description = @description, Date = @date, Location = @location WHERE Id = @id";
                using var command = new SqliteCommand(query, connection);
                command.Parameters.Add(CreateParameter("@name", name));
                command.Parameters.Add(CreateParameter("@description", description));
                command.Parameters.Add(CreateParameter("@date", date.ToString("yyyy-MM-dd")));
                command.Parameters.Add(CreateParameter("@location", location));
                command.Parameters.Add(CreateParameter("@id", id));
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0 ? "Event updated successfully." : "Event not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating event: {ex.Message}");
            }
        }
    }
}
