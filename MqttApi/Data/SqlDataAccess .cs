using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace MqttApi.Data
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

        //public async Task SaveData<T>(
        //    string storedPrecedure,
        //    T parameters,
        //    string connectionId = "Default")

        //{
        //    using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
        //    var result = await connection.QuerySingleOrDefaultAsync(storedPrecedure, parameters, commandType: CommandType.StoredProcedure);
        //    var json = JsonConvert.SerializeObject(result);
        //    Customer person = JsonConvert.DeserializeObject<Customer>(json);

        //}
    }
}