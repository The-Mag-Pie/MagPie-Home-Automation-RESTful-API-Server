using MagPie_Home_Automation_RESTful_API_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagPie_Home_Automation_RESTful_API_Server.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestEndpoint : ControllerBase
    {
        private class ResponseModel
        {
            public int RandomTestInt { get; set; }
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = new ResponseModel()
            {
                RandomTestInt = new Random().Next(-100, 100)
            };

            return Ok(new EndpointResponse(true)
            {
                Data = response,
                Message = "SERVER IS WORKING"
            });
        }
    }
}
