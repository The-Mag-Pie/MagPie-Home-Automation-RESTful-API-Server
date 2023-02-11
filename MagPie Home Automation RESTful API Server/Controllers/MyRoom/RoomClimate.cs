using MagPie_Home_Automation_RESTful_API_Server.BackgroundTasks;
using MagPie_Home_Automation_RESTful_API_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagPie_Home_Automation_RESTful_API_Server.Controllers.MyRoom
{
    [Route("room/climate")]
    [ApiController]
    public class RoomClimate : ControllerBase
    {
        private class ResponseModel
        {
            public double? Temperature { get; set; }
            public double? Humidity { get; set; }
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (RoomClimateReader.TemperatureCelsius == null || RoomClimateReader.Humidity == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new EndpointResponse(false)
                {
                    Message = "An error occurred while getting data from the sensor."
                });
            }

            var response = new ResponseModel()
            {
                Temperature = RoomClimateReader.TemperatureCelsius,
                Humidity = RoomClimateReader.Humidity
            };

            return Ok(new EndpointResponse(true)
            {
                Data = response
            });
        }
    }
}
