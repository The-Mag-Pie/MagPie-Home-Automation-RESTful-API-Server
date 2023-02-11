using MagPie_Home_Automation_RESTful_API_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MagPie_Home_Automation_RESTful_API_Server.Controllers.MyRoom.Raspberry_Pi
{
    [Route("room/raspberrypi/{Command}")]
    [ApiController]
    public class ShutdownReboot : ControllerBase
    {
        [BindProperty(SupportsGet = true)]
        public string Command { get; set; } = string.Empty;

        [HttpGet]
        public IActionResult Get()
        {
            var process = new Process();
            process.StartInfo.UseShellExecute = false;

            process.StartInfo.FileName = "bash";
            process.StartInfo.Arguments = "-c \"sleep 2; ";

            var endpointResponse = new EndpointResponse(true);

            switch (Command)
            {
                case "shutdown":
                    process.StartInfo.Arguments += "sudo shutdown 0;\"";
                    endpointResponse.Message = "Raspberry Pi is shutting down...";
                    break;

                case "reboot":
                    process.StartInfo.Arguments += "sudo shutdown -r 0;\"";
                    endpointResponse.Message = "Raspberry Pi is rebooting...";
                    break;

                default:
                    return NotFound();
            }

            process.Start();

            return Ok(endpointResponse);
        }
    }
}
