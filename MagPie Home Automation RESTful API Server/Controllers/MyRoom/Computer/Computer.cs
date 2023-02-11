using MagPie_Home_Automation_RESTful_API_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagPie_Home_Automation_RESTful_API_Server.Controllers.MyRoom.Computer
{
    [Route("room/computer/{Command}")]
    [ApiController]
    public class Computer : ControllerBase
    {
        private struct Commands
        {
            public const string Shutdown = "shutdown";
            public const string Restart = "restart";
            public const string UefiRestart = "uefirestart";
            public const string AdvBootOptionsRestart = "advbootoptionsrestart";
        }

        private class ResponseModel
        {
            public string ServerReply { get; set; } = string.Empty;
        }
        
        [BindProperty(SupportsGet = true)]
        public string Command { get; set; } = string.Empty;

        [HttpGet]
        public IActionResult Get()
        {
            string socketCommand, responseMessage;
            switch (Command)
            {
                case Commands.Shutdown:
                    socketCommand = "shutdown";
                    responseMessage = "Computer is shutting down...";
                    break;

                case Commands.Restart:
                    socketCommand = "restart";
                    responseMessage = "Computer is restarting...";
                    break;

                case Commands.UefiRestart:
                    socketCommand = "uefi";
                    responseMessage = "Computer is restarting to UEFI...";
                    break;

                case Commands.AdvBootOptionsRestart:
                    socketCommand = "advbootopt";
                    responseMessage = "Computer is restarting to Windows Advanced Boot Options...";
                    break;

                default:
                    return NotFound();
            }

            string socketResponse = TCPSocketClient.SendCommand(socketCommand, ControlTypes.ComputerControls) ?? "error";

            var response = new ResponseModel()
            {
                ServerReply = socketResponse
            };

            if (socketResponse == "error" || socketResponse == "Internal error" || socketResponse == string.Empty)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new EndpointResponse(false)
                {
                    Data = response,
                    Message = "Internal server error"
                });
            }

            return Ok(new EndpointResponse(true)
            {
                Message = responseMessage,
                Data = response
            });
        }
    }
}
