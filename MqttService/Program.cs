using MqttService;
using MqttService.Data;
using MqttService.Entity;
using MqttService.Repository;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

Main();

static void Main()
{
    var timer = new System.Threading.Timer(MainApp, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}

static void MainApp(object state)
{
    Random random = new Random();
    Console.WriteLine("test");
    MqttClient mqttService = new MqttClient("192.168.0.108", 1883, false, MqttSslProtocols.None, null, null);
    Console.WriteLine(mqttService.Settings);
    mqttService.Connect(new Guid().ToString());
    mqttService.MqttMsgPublishReceived += Subscribe;

    DateTime currentDateTime = DateTime.Now;
    Gauge gauge1 = new Gauge
    {
        Id = 1,
        Data = random.Next(1, 101),
        Date = GetRandomDateTime(),
        GaugeId = 1
    };
    currentDateTime = DateTime.Now;
    Gauge gauge2 = new Gauge
    {
        Id = 1,
        Data = random.Next(1, 101),
        Date = GetRandomDateTime(),
        GaugeId = 2
    };
    string gauge1Json = JsonConvert.SerializeObject(gauge1);
    string gauge2Json = JsonConvert.SerializeObject(gauge2);

    mqttService.Publish("stajyer/gauge1", Encoding.UTF8.GetBytes(gauge1Json), 0, true);
    mqttService.Publish("stajyer/gauge2", Encoding.UTF8.GetBytes(gauge2Json), 0, true);

    mqttService.Subscribe(new string[] { "stajyer/gauge1" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    mqttService.Subscribe(new string[] { "stajyer/gauge2" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
}


static void Subscribe(object sender, MqttMsgPublishEventArgs e)
{
    GaugeRepository repository = new GaugeRepository();
    string utfString = Encoding.UTF8.GetString(e.Message, 0, e.Message.Length);
    Gauge gauge = JsonConvert.DeserializeObject<Gauge>(utfString);
    repository.Insert(gauge);
}


static DateTime GetRandomDateTime()
{
    DateTime currentDate = DateTime.Today; 
    int currentWeek = GetIso8601WeekOfYear(currentDate);
    Random random = new Random();

    DateTime firstDayOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek);

    DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);

    int randomDays = random.Next(0, (int)(lastDayOfWeek - firstDayOfWeek).TotalDays + 1);

    DateTime randomDateTime = firstDayOfWeek.AddDays(randomDays);

    Console.WriteLine("Random DateTime within the current week: " + randomDateTime);

    return randomDateTime;
}

static int GetIso8601WeekOfYear(DateTime date)
{
    DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
    if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
    {
        date = date.AddDays(3);
    }

    return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
}



