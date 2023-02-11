using Iot.Device.DHTxx;

namespace MagPie_Home_Automation_RESTful_API_Server.BackgroundTasks
{
    public class RoomClimateReader : Task
    {
        public static double? TemperatureCelsius { get; private set; } = null;
        public static double? Humidity { get; private set; } = null;

        private const int pin = 4;

        public RoomClimateReader() : base(() =>
        {
            using var dht11 = new Dht11(pin);

            Console.WriteLine("Climate reader started.");
            while (true)
            {
                bool success;

                success = dht11.TryReadTemperature(out var temp);
                if (success == false)
                    continue;

                success = dht11.TryReadHumidity(out var humidity);
                if (success == false)
                    continue;

                TemperatureCelsius = temp.DegreesCelsius;
                Humidity = humidity.Percent;
                Thread.Sleep(5000);
            }
        })
        {
            Start();
        }
    }
}
