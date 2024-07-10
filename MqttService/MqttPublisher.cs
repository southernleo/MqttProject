using MqttService.Entity;
using Newtonsoft.Json;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MqttService
{
    public class MqttPublisher
    {
        MqttClient mqttPublisher;

        public MqttPublisher()
        {
            //initilize();    
        }


        private void initilize()
        {
            mqttPublisher = new MqttClient("192.168.0.108", 1883, false, MqttSslProtocols.None, null, null);

            mqttPublisher.Connect(new Guid().ToString());



        }

        public void publish(string topic, string text)
        {
            mqttPublisher = new MqttClient("192.168.0.108", 1883, false, MqttSslProtocols.None, null, null);

            mqttPublisher.Connect(new Guid().ToString());

            //string json = JsonConvert.SerializeObject(gauge);
            mqttPublisher.Publish(topic, Encoding.UTF8.GetBytes(text), 0, true);

            
        }
    }
}
