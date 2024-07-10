using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace MqttService.Data
{
    public class SqlDataAccess : ISqlDataAccess
    {

        public SqlDataAccess()
        {
        }

        public async Task<IEnumerable<T>> Execute<T, U>(
            string storedPrecedure,
            U parameters,
            string connectionId = "Default")
        {
            using IDbConnection connection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Gauge;TrustServerCertificate=true;Trusted_Connection=True;Integrated Security=SSPI;MultipleActiveResultSets=true;");
            return await connection.QueryAsync<T>(storedPrecedure, parameters, commandType: CommandType.StoredProcedure);
        }

    }
}