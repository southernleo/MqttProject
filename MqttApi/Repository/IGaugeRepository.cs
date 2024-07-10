using MqttApi.Entity;

namespace MqttApi.Repository
{
    public interface IGaugeRepository
    {
        Task<IEnumerable<Gauge>?> GetAll();
        Task<Gauge> Insert(Gauge gauge);
    }
}