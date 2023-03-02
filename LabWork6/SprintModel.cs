using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork6
{
    class SprintModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }
        public bool IsActive4User { get; set; }

        public static SprintModel CheckSprint(int userId)
        {
            var sprintModel = new SprintModel();
            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                var command = new SqlCommand("p_checkSprint", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sprintModel.Id = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]);
                        sprintModel.Name = reader["Name"].ToString();
                        sprintModel.BeginDate = reader["BeginDate"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["BeginDate"]);
                        sprintModel.EndDate = reader["EndDate"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(reader["EndDate"]);
                        sprintModel.IsActive4User = reader["IsActiveForUser"] != DBNull.Value && Convert.ToBoolean(reader["IsActiveForUser"]);
                    }
                }
            }

            return sprintModel;
        }

        public static string InsertSprint(int taskNumb,string comment,int userId)
        {
            string result = String.Empty;

            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Desktop\3sem\OOP\DaryaGanzha\LabWork6\Database1.mdf;Integrated Security=True"))
            {
                connection.Open();
                var command = new SqlCommand("p_insertSprint", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@taskNumb", taskNumb);
                command.Parameters.AddWithValue("@comment", comment);
                command.ExecuteNonQuery();
            }

            return result;
        }
    }
}
