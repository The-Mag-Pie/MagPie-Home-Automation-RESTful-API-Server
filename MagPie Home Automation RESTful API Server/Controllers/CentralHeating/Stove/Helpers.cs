namespace MagPie_Home_Automation_RESTful_API_Server.Controllers.CentralHeating.Stove
{
    public class Helpers
    {
        public static string URL
        {
            get
            {
                var envvar = Environment.GetEnvironmentVariable("EWELINK_PROXY_URL");
                return envvar ?? "http://127.0.0.1:8123";
            }
        }
    }
}
