using MagPie_Home_Automation_RESTful_API_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagPie_Home_Automation_RESTful_API_Server.Controllers.CentralHeating
{
    [Route("centralheating/watertemperature")]
    [ApiController]
    public class WaterTemperature : ControllerBase
    {
        private static string SensorURL
        {
            get
            {
                var envvar = Environment.GetEnvironmentVariable("WATER_TEMP_SENSOR_URL");
                return envvar ?? throw new ArgumentNullException("Missing WATER_TEMP_SENSOR_URL environment variable");
            }
        }

        private class ResponseModel
        {
            public double Temperature { get; set; }
        }

        [HttpGet]
        public IActionResult Get()
        {
            HttpClient client = HttpRequestClient.httpClient;

            try
            {
                string responseBody = client.GetStringAsync(SensorURL).Result;

                var response = new ResponseModel()
                {
                    Temperature = Convert.ToDouble(responseBody)
                };

                return Ok(new EndpointResponse(true)
                {
                    Data = response
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new EndpointResponse(false)
                {
                    Message = e.Message
                });
            }
        }
    }
}
