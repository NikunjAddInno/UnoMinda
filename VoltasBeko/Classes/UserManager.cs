using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltasBeko.Classes
{
    public static class UserManager
    {

        public enum UserRoles
        {
            Admin,
            Production,
            RnD,
            Quality,
            IT,
            Maintenance,
            Operator
        }

        public static event Action UserRoleChanged;
        private static UserRoles _userRole = UserRoles.Admin;
        public static UserRoles UserRole
        {
            get { return _userRole; }
            set
            {
                _userRole = value;
                UserRoleChanged?.Invoke();
            }
        }

        public static void CreateUser(string userId, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentException("UserId, Password, and Role cannot be blank.");
            }

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Users (UserId, Password, Role) VALUES (@UserId, @Password, @Role)";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Role", role);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool LoginUser(string userId, string password)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string query = "SELECT UserId, Role FROM Users WHERE UserId = @UserId AND Password = @Password";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string userName = reader["UserId"].ToString();
                            string roleString = reader["Role"].ToString();

                            if (Enum.TryParse(roleString, out UserRoles role))
                            {
                                UserRole = role;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }

        public static void UpdatePassword(string userId, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException("Password cannot be blank.");
            }

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Users SET Password = @Password WHERE UserId = @UserId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Password", newPassword);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteUser(string userId)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Users WHERE UserId = @UserId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
