using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork6
{
    class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int AssignedUserId { get; set; }
        public string Comment { get; set; }
        public string Description { get; set; }

        public int StatusId { get; set; }
        public DateTime LastChangedDate { get; set; }
        public int LastChangedUserId { get; set; }

        public static void CreateNewTAsk(string taskName,string description,int userId)
        {
            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                string date = DateTime.Now.ToString("s");
                var command = new SqlCommand($"insert into Tasks values ('{taskName}','{date}',null,null,'{description}',1,'{date}',{userId})", connection);
                command.ExecuteNonQuery();
            }
        }

        public static string ChangeTaskStatus (int taskId, int statusId,int userId)
        {
            string result = String.Empty;
            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                var command = new SqlCommand("ChangeTaskStatus", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TaskId", taskId);
                command.Parameters.AddWithValue("@statusId", statusId);
                command.Parameters.AddWithValue("@UserId", userId);
               SqlDataReader reader=command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = reader["result"].ToString();
                    }
                }
            }
            return result;
        }

        public static List<TaskModel> GetAllTasks()
        {
            var result = new List<TaskModel>();
            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                var command = new SqlCommand("select * from Tasks", connection);
                SqlDataReader reader= command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new TaskModel();
                        model.Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]);
                        model.Name = reader["Name"].ToString();
                        model.CreationDate = reader["CreationDate"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["CreationDate"]);
                        model.AssignedUserId = reader["AssignedUserId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AssignedUserId"]);
                        model.Comment = reader["Comment"].ToString();
                        model.Description = reader["Description"].ToString();
                        model.StatusId = reader["StatusId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["StatusId"]);
                        model.LastChangedDate = reader["LastChangedDate"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["LastChangedDate"]);
                        model.LastChangedUserId = reader["LastChangedUserId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LastChangedUserId"]);
                        result.Add(model);
                    }
                }
            }

            return result;
        }

        public static TaskModel GetTaskById(int taskId)
        {
            var model = new TaskModel();
            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                var command = new SqlCommand($"select * from Tasks where id={taskId}", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        model.Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]);
                        model.Name = reader["Name"].ToString();
                        model.CreationDate = reader["CreationDate"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["CreationDate"]);
                        model.AssignedUserId = reader["AssignedUserId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AssignedUserId"]);
                        model.Comment = reader["Comment"].ToString();
                        model.Description = reader["Description"].ToString();
                        model.StatusId = reader["StatusId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["StatusId"]);
                        model.LastChangedDate = reader["LastChangedDate"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["LastChangedDate"]);
                        model.LastChangedUserId = reader["LastChangedUserId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LastChangedUserId"]);
                    }
                }
            }

            return model;
        }

        public static List<TaskModel> GetMyTasks(int userId)
        {
            var result = new List<TaskModel>();
            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                var command = new SqlCommand($"select * from Tasks where AssignedUserId={userId}", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new TaskModel();
                        model.Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]);
                        model.Name = reader["Name"].ToString();
                        model.CreationDate = reader["CreationDate"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["CreationDate"]);
                        model.AssignedUserId = reader["AssignedUserId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AssignedUserId"]);
                        model.Comment = reader["Comment"].ToString();
                        model.Description = reader["Description"].ToString();
                        model.StatusId = reader["StatusId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["StatusId"]);
                        model.LastChangedDate = reader["LastChangedDate"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["LastChangedDate"]);
                        model.LastChangedUserId = reader["LastChangedUserId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LastChangedUserId"]);
                        result.Add(model);
                    }
                }
            }

            return result;
        }
    }
}
