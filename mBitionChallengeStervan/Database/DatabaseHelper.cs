using Microsoft.Data.Sqlite;

namespace mBitionChallengeStervan.database
{
    public static class DatabaseHelper
    {
        private const string ConnectionString = "Data Source=events.db;";

        public static void InitializeDatabase()
        {
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();

                string tableQuery = @"
                CREATE TABLE IF NOT EXISTS Events (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    Date TEXT NOT NULL CHECK (Date GLOB '????-??-??'),
                    Location TEXT
                );";

                using var command = new SqliteCommand(tableQuery, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization failed: {ex.Message}");
            }
        }

        public static SqliteConnection GetConnection()
        {
            var connection = new SqliteConnection(ConnectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection error: {ex.Message}");
                throw;
            }
            return connection;
        }
    }
}
