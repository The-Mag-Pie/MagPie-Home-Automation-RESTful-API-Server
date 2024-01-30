namespace MagPie_Home_Automation_RESTful_API_Server.Controllers.CentralHeating.Stove
{
    public class Helpers
    {
        public static string URL => Environment.GetEnvironmentVariable("EWELINK_PROXY_URL") ?? "http://127.0.0.1:8123";

        public static string DeviceID => Environment.GetEnvironmentVariable("EWELINK_DEVICE_ID") ?? "10006ccbb6";
    }
}
