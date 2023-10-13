using MagPie_Home_Automation_RESTful_API_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagPie_Home_Automation_RESTful_API_Server.Controllers.CentralHeating.Stove
{
    [Route("centralheating/stove/toggle")]
    [ApiController]
    public class Toggle : ControllerBase
    {
        private static readonly string APIURL = $"{Helpers.URL}/toggle";

        [HttpGet]
        public IActionResult Get()
        {
            HttpClient client = HttpRequestClient.httpClient;

            try
            {   
                string responseBody = client.GetStringAsync(APIURL).Result;

                if (responseBody == "OK")
                    return Ok(new EndpointResponse(true)
                    {
                        Message = "Stove power state has been toggled."
                    });
                else
                    throw new Exception("Bad response: " + responseBody);
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
