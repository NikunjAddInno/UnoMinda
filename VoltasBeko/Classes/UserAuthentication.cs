using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;

namespace VoltasBeko
{
    public static class UserAuthentication
    {
        public static readonly string connectionString = @"Server=localhost; Port = 5432; user Id = postgres; password = 1234; Database = postgres;";
        //public NpgsqlConnection GetConnection()
        //{
        //    return new NpgsqlConnection(@"Server=localhost; Port = 5432; user Id = postgres; password = 1234; Database = postgres;");
        //}

        static UserAuthentication()
        {

        }

        public static void CreateUser(string username, string password)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Check if the user already exists
                if (UserExists(connection, username))
                {
                    MessageBox.Show("User already exists.");
                    Console.WriteLine("User already exists.");
                    return;
                }

                // Insert the new user with the provided password (without hashing)
                string insertQuery = "INSERT INTO user_credentials (username, password) VALUES (@username, @password)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("password", password);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("New user created.");

            }
        }

        public static bool UserExists(NpgsqlConnection connection, string username)
        {
            string selectQuery = "SELECT COUNT(*) FROM user_credentials WHERE username = @username";
            using (NpgsqlCommand cmd = new NpgsqlCommand(selectQuery, connection))
            {
                cmd.Parameters.AddWithValue("username", username);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        public static bool VerifyUserCredentials(string username, string password)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Check if the user exists
                if (!UserExists(connection, username))
                {
                    MessageBox.Show("User does not exist.");

                    Console.WriteLine("User does not exist.");
                    return false;
                }

                // Retrieve the stored password for the user (without hashing)
                string selectQuery = "SELECT password FROM user_credentials WHERE username = @username";
                using (NpgsqlCommand cmd = new NpgsqlCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    string storedPassword = cmd.ExecuteScalar() as string;

                    // Verify the input password against the stored password
                    if (storedPassword == password)
                    {
                        MessageBox.Show("Password is correct.");
                        Console.WriteLine("Password is correct.");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Password is incorrect.");
                        Console.WriteLine("Password is incorrect.");
                        return false;
                    }
                }
            }
        }
    }
}
