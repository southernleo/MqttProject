namespace MqttApi.Data
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> Execute<T, U>(string storedPrecedure, U parameters, string connectionId = "Default");
    }
}