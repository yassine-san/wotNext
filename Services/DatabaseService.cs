using System;
using System.Data;
using System.Data.Sql;
using System.IO;
using Microsoft.Data.Sqlite;

namespace wotNext.Services
{
    public class DatabaseService
    {
        private const string DbFileName = "database.db"; // Change this to your desired database file name
        private const string ConnectionString = "Data Source=" + DbFileName;

        public static bool IsDbExist()
        {
            return File.Exists(DbFileName);
        }

        public static int CreateDatabase()
        {
            if (IsDbExist())
            {
                Console.WriteLine("Database already exists.");
                return 0;
            }

            try
            {
                using (var connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open(); // This will create the database file
                }
                Console.WriteLine("Database created successfully.");
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating the database: {ex.Message}");
                return -1;
            }
        }

        public static int HandleLogin(string username, string hashedPassword)
        {
            if (!IsDbExist())
            {
                Console.WriteLine("Database does not exist. Please create the database first.");
                return -1;
            }

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(connection.ConnectionString))
                {
                    command.CommandText = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", hashedPassword);

                    int userCount = Convert.ToInt32(command.ExecuteScalar());

                    if (userCount == 1)
                    {
                        Console.WriteLine("Login successful!");
                        return 1; // User authenticated successfully
                    }
                    else
                    {
                        Console.WriteLine("Login failed. Invalid username or password.");
                        return 0; // Authentication failed
                    }
                }
            }
        }
    }
}
