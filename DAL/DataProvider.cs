using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;
namespace LTTQ_G2_2025.DAL
{
    //private string connectionString = "Data Source=.\\SQLExpress;Initial Catalog=ThesisManagement;User ID=sa;Encrypt=True;Trust Server Certificate=True";

    public class DataProvider
    {
        private static DataProvider instance;
        private readonly string connectionString;

        private DataProvider()
        {
            var settings = ConfigurationManager.ConnectionStrings["MyConnectionString"];
            if (settings != null)
            {
                connectionString = settings.ConnectionString;
            }
            else
            {
                throw new Exception("Connection string 'MyConnectionString' not found");
            }
        }

        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataProvider();

                return instance;
            }
            private set => instance = value;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public int ExecuteNonQuery(string query, object[] parameters = null)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                AddParameters(query, parameters, command);

                return command.ExecuteNonQuery();
            }
        }

        public DataTable ExecuteQuery(string query, object[] parameters = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                AddParameters(query, parameters, command);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
            }

            return data;
        }

        public object ExecuteScalar(string query, object[] parameters = null)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                AddParameters(query, parameters, command);

                return command.ExecuteScalar();
            }
        }

        private void AddParameters(string query, object[] parameters, SqlCommand command)
        {
            if (parameters == null)
                return;

            MatchCollection matches = Regex.Matches(query, @"@\w+");

            if (matches.Count != parameters.Length)
                throw new Exception("Số lượng parameters không khớp với số tham số trong câu SQL!");

            for (int i = 0; i < matches.Count; i++)
            {
                string paramName = matches[i].Value;
                command.Parameters.AddWithValue(paramName, parameters[i] ?? DBNull.Value);
            }
        }
        public DataTable ExecuteQuery(string sql, IDictionary<string, object> namedParams)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                // trích tên tham số như trên...
                var names = new List<string>();
                var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (Match m in Regex.Matches(sql, @"(?<!@)@\w+"))
                {
                    string name = m.Value;
                    if (seen.Add(name)) names.Add(name);
                }

                foreach (var n in names)
                {
                    object val;
                    if (namedParams != null && namedParams.TryGetValue(n, out val))
                        cmd.Parameters.AddWithValue(n, val ?? DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue(n, DBNull.Value); // hoặc throw nếu bắt buộc
                }

                var dt = new DataTable();
                conn.Open();
                da.Fill(dt);
                return dt;
            }
        }

    }
}
