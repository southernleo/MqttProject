using MqttService.Entity;
using MqttService.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MqttService
{
    public class MqttSubscriber
    {

        MqttClient mqttSubscriber;
        GaugeRepository gaugeRepository;


        public MqttSubscriber(string topic)
        {
            initilize(topic);
            this.gaugeRepository = new GaugeRepository();
        }


        private void initilize(string topic)
        {
            mqttSubscriber = new MqttClient("192.168.0.108", 1883, false, MqttSslProtocols.None, null, null);

            mqttSubscriber.Connect(new Guid().ToString());

            mqttSubscriber.MqttMsgPublishReceived += Subscribe;

            mqttSubscriber.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });


        }

        public void Subscribe(object sender, MqttMsgPublishEventArgs e)
        {
            string utfString = Encoding.UTF8.GetString(e.Message, 0, e.Message.Length);
            //Gauge gauge = JsonConvert.DeserializeObject<Gauge>(utfString);

            Console.WriteLine(utfString);
        }

    }
}
