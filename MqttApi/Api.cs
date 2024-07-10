
using MqttApi.Repository;

namespace MqttApi
{
    public static class Api
    {
        public static void ConfigureApi(this WebApplication app)
        {
            app.MapGet("/gauge", GetGaugeData)
        }

        private static async Task<IResult> GetGaugeData(IGaugeRepository data)
        {
            try
            {
                return Results.Ok(await data.GetAll());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        
    }
}