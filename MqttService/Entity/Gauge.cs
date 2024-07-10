using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttService.Entity
{
    public class Gauge
    {

        public int Id { get; set; }
        public int Data { get; set; }
        public DateTime Date { get; set; }
        public int GaugeId { get; set; }
    }
}
