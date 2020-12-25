using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManager
{
    class DbManager
    {
        private readonly SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-2CSILTI\PC;Initial Catalog=Tasks;Integrated Security=True");
        //Get Info
        public DataTable GetAllTasks()
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand($"GetTasks", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            var inf = dt;
            connection.Close();
            return dt;
        }
        // Filter Tasks
        public DataTable GetFilteredTasks(string s)
        {
            DataTable dt = new DataTable();
            string query = "";
            if (s == "Active") { query = "GetActiveTasks"; }
            else if (s == "Completed") { query = "GetInActiveTasks"; }
            else { query = "GetTasks"; }
            connection.Open();
            SqlCommand cmd = new SqlCommand($"{query}", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            dt.Load(cmd.ExecuteReader());
            connection.Close();
            return dt;

        }
        // Update Tasks
        public void UpdateTask(int id, DateTime date, int status)
        {
            try
            {
                string query = $"Update Task Set performed_date ='{date}', status ={status} Where task_ID={id}";
                connection.Open();
                SqlDataAdapter sda = new SqlDataAdapter(query, connection);
                sda.SelectCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Invalid");
            }

        }
        // Tasks By ID
        public DataTable GetTasksById(int id)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("GetTasksByID", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", id);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();
            return dt;

        }
        // Tasks By Name
        public DataTable GetTasksByName(string name)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("GetTasksByName", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();
            return dt;

        }
        public List<Employee> GetEmployees()
        {
            var list = new List<Employee>();
            SqlCommand cmd = new SqlCommand("GetEmployees", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var empl = new Employee();
                    empl.ID = int.Parse(reader["employee_ID"].ToString());
                    empl.name = reader["full_name"].ToString();
                    empl.privateNumber = reader["private_number"].ToString();
                    list.Add(empl);
                }
            }

            connection.Close();
            return list;

        }
        // Add Task
        public void AddTask(DateTime creation, DateTime due, string desc)
        {
            connection.Open();
            string query = $"Insert Into Task Values('{creation}','{due}',default,'{desc}',default)";
            SqlDataAdapter cmd = new SqlDataAdapter(query,connection);
            cmd.SelectCommand.ExecuteNonQuery();
            connection.Close();
        }
        // Add Relationship
        public void AddRelationship(int task, int empl)
        {
            connection.Open();
            string query = $"Insert Into Executor Values ({task}, {empl})";
            SqlDataAdapter cmd = new SqlDataAdapter(query, connection);
            cmd.SelectCommand.ExecuteNonQuery();
            connection.Close();
        }

    }
}
