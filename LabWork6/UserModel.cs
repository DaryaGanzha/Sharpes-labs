using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork6
{
    internal class UserModel
    {
        public int Id { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public int? ParentId { get; set; }
        public List<UserModel> Children { get; set; }

        public  static UserModel CheckAuthorizeUser(string login, string password)
        {
            var model = new UserModel();

            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                var command = new SqlCommand($"select * from Users where UserLogin='{login}' and USerPassword='{password}'", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        model.Id = Convert.ToInt32(reader["Id"]);
                        model.UserLogin = reader["UserLogin"].ToString();
                        model.UserPassword = reader["USerPassword"].ToString();
                        model.ParentId = reader["UserParentId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserParentId"]);
                        model.Children = new List<UserModel>();
                    }
                }
            }

            return model;
        }

        public static string CreateUser(string login,string password,int parentId)
        {
            string errorMessage = String.Empty;
            try
            {
                using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
                {
                    connection.Open();
                    var command = new SqlCommand($"insert into Users values ('{login}','{password}',{parentId})", connection);
                    command.ExecuteNonQuery();
                }
            }
            catch(Exception e)
            {
                errorMessage = e.Message;
            }
            return errorMessage;

        }

        public static UserModel GetSubordinatesByParentId (int userId)
        {
            var users = new List<UserModel>();
            var sortedList = new UserModel();
            try
            {
                using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
                {
                    connection.Open();
                    var command = new SqlCommand("p_GetSubordinates", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var model = new UserModel();
                            model.Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]);
                            model.UserLogin = reader["UserLogin"].ToString();
                            model.UserPassword = reader["UserPassword"].ToString();
                            model.ParentId = reader["UserParentId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserParentId"]);

                            users.Add(model);
                        }
                    }
                }

                Action<UserModel> config = null;
                config = model =>
                {
                    model.Children = users.Where(w => w.ParentId == model.Id).ToList();
                    foreach (UserModel child in model.Children)
                        config(child);
                };

                sortedList = users.FirstOrDefault(w => w.Id == userId);
                config(sortedList);

            }
            catch (Exception e)
            {
                // ignored
            }

            return sortedList;
        }

        public static UserModel GetSubordinates()
        {
            var users = new List<UserModel>();
            var sortedList = new UserModel();
            try
            {
                using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
                {
                    connection.Open();
                    var command = new SqlCommand("p_GetSubordinatesWithoutParentId", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var model = new UserModel();
                            model.Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]);
                            model.UserLogin = reader["UserLogin"].ToString();
                            model.UserPassword = reader["UserPassword"].ToString();
                            model.ParentId = reader["UserParentId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserParentId"]);

                            users.Add(model);
                        }
                    }


                }


                Action<UserModel> config = null;
                config = model =>
                {
                    model.Children = users.Where(w => w.ParentId == model.Id).ToList();
                    foreach (UserModel child in model.Children)
                        config(child);
                };

                sortedList = users.FirstOrDefault(w => w.ParentId == 0);
                config(sortedList);

            }
            catch (Exception e)
            {
                // ignored
            }

            return sortedList;
        }

        public static void SetAdministrator(int selectedUserId,int selectedAdministrator)
        {
            try
            {
                using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
                {
                    connection.Open();
                    var command = new SqlCommand($"update Users set UserParentId={selectedAdministrator} where id={selectedUserId}", connection);
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }


        public static UserModel GetWorkerById(int workerId)
        {
            var model = new UserModel();

            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                var command = new SqlCommand($"select * from Users where id='{workerId}'", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        model.Id = Convert.ToInt32(reader["Id"]);
                        model.UserLogin = reader["UserLogin"].ToString();
                        model.UserPassword = reader["USerPassword"].ToString();
                        model.ParentId = reader["UserParentId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["UserParentId"]);
                        model.Children = new List<UserModel>();
                    }
                }
            }

            return model;
        }

        public static void DeleteUser(int workerId,int currentUserId)
        {
            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                var command = new SqlCommand("p_deleteUser", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", workerId);
                command.Parameters.AddWithValue("@CurUserId", currentUserId);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdatePassword(int userId,string newPassword)
        {
            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                var command = new SqlCommand($"update Users set UserPassword={newPassword} where Id={userId}", connection);
                command.ExecuteNonQuery();
            }
        }

    }
}
