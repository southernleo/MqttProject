using MqttApi.Data;
using MqttApi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttApi.Repository
{
    public class GaugeRepository : IGaugeRepository
    {
        private readonly ISqlDataAccess _db;

        public GaugeRepository()
        {
            _db = new SqlDataAccess();
        }

        public async Task<IEnumerable<Gauge>?> GetAll()
        {
            return await _db.Execute<Gauge, dynamic>("dbo.spGauge_GetAll", new {});
        }

        public async Task<Gauge> Insert(Gauge gauge)
        {
            var results = await _db.Execute<Gauge, dynamic>("dbo.spGauge_Insert", new { gauge.Data, gauge.Date, gauge.GaugeId });
            return results.FirstOrDefault();

        }

    }
}
