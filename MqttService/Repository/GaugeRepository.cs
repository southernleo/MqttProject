using MqttService.Data;
using MqttService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttService.Repository
{
    public class GaugeRepository
    {
        private readonly ISqlDataAccess _db;

        public GaugeRepository()
        {
            _db = new SqlDataAccess();
        }

        public async Task<Gauge?> Get(int id)
        {
            var results = await _db.Execute<Gauge, dynamic>("dbo.spCustomer_Get", new { Id = id });
            return results.FirstOrDefault();
        }

        public async Task<Gauge> Insert(Gauge gauge)
        {
            var results = await _db.Execute<Gauge, dynamic>("dbo.spGauge_Insert", new { gauge.Data, gauge.Date, gauge.GaugeId });
            return  results.FirstOrDefault();

        }

    }
}
