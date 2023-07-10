using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.SqlActions
{
    public class DbAccessHelper
    {
        public static DataTable ExecuteCommand(SqlCommand command)
        {
            var connstringBuilder = new SqlConnectionStringBuilder("Data Source=.\\MSSQLSERVER01;Initial Catalog=AnagramSolverData;Integrated Security=True");
            DataTable dataTable = new();
            using SqlConnection connection = new(connstringBuilder.ConnectionString);
            connection.Open();
            command.Connection = connection;
            using SqlDataAdapter dataAdapter = new(command);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}
