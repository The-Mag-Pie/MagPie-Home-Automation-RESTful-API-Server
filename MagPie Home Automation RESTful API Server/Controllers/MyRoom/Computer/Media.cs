using MagPie_Home_Automation_RESTful_API_Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace MagPie_Home_Automation_RESTful_API_Server.Controllers.MyRoom.Computer
{
    [Route("room/computer/media/{Command}")]
    [ApiController]
    public class Media : ControllerBase
    {
        private struct Commands
        {
            public const string PlayPause = "playpause";
            public const string VolumeUp = "volumeup";
            public const string VolumeDown = "volumedown";
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
                case Commands.PlayPause:
                    socketCommand = "play_pause";
                    responseMessage = "PLAY/PAUSE button pressed.";
                    break;

                case Commands.VolumeUp:
                    socketCommand = "volup";
                    responseMessage = "Volume up button pressed.";
                    break;

                case Commands.VolumeDown:
                    socketCommand = "voldown";
                    responseMessage = "Volume down button pressed";
                    break;

                default:
                    return NotFound();
            }

            string socketResponse = TCPSocketClient.SendCommand(socketCommand, ControlTypes.MediaControls) ?? "error";

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
