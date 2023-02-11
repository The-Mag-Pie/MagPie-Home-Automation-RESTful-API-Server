using MagPie_Home_Automation_RESTful_API_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagPie_Home_Automation_RESTful_API_Server.Controllers.CentralHeating.Stove
{
    [Route("centralheating/stove/state")]
    [ApiController]
    public class State : ControllerBase
    {
        private const string APIURL = "http://127.0.0.1:8123/getstatus";

        private class ResponseModel
        {
            public string State { get; set; } = PowerStates.Unknown;
        }

        [HttpGet]
        public IActionResult Get()
        {
            HttpClient client = HttpRequestClient.httpClient;

            try
            {
                string responseBody = client.GetStringAsync(APIURL).Result;

                var response = new ResponseModel();

                if (responseBody == "on")
                    response.State = PowerStates.On;
                else if (responseBody == "off")
                    response.State = PowerStates.Off;
                else
                    throw new Exception("Bad response: " + responseBody);

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
