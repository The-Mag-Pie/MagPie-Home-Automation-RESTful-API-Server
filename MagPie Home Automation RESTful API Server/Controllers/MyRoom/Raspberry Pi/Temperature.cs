using MagPie_Home_Automation_RESTful_API_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MagPie_Home_Automation_RESTful_API_Server.Controllers.MyRoom.Raspberry_Pi
{
    [Route("room/raspberrypi/temperature")]
    [ApiController]
    public class Temperature : ControllerBase
    {
        private class ResponseModel
        {
            public double Temp1 { get; set; }
            public double Temp2 { get; set; }
        }

        [HttpGet]
        public IActionResult Get()
        {
            double temp1, temp2;

            var process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;

            process.StartInfo.FileName = "vcgencmd";
            process.StartInfo.Arguments = "measure_temp";
            process.Start();
            process.WaitForExit();
            var output1 = process.StandardOutput.ReadToEnd();
            output1 = output1.Replace("temp=", "").Replace("'C\n", "");

            process.StartInfo.FileName = "cat";
            process.StartInfo.Arguments = "/sys/class/thermal/thermal_zone0/temp";
            process.Start();
            process.WaitForExit();
            var output2 = process.StandardOutput.ReadToEnd();
            output2 = output2.Replace("\n", "").Insert(2, ".").Remove(4);

            if (double.TryParse(output1, out temp1) == false ||
                double.TryParse(output2, out temp2) == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new EndpointResponse(false)
                {
                    Message = "Internal server error"
                });
            }

            var response = new ResponseModel()
            {
                Temp1 = temp1,
                Temp2 = temp2
            };

            return Ok(new EndpointResponse(true)
            {
                Data = response
            });
        }
    }
}
