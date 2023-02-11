using MagPie_Home_Automation_RESTful_API_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Device.Gpio;

namespace MagPie_Home_Automation_RESTful_API_Server.Controllers.MyRoom.ShelfLed
{
    [Route("room/shelfled/switch")]
    [ApiController]
    public class Switch : ControllerBase
    {
        private class ResponseModel
        {
            public string OldState { get; set; }
            public string NewState { get; set; }
        }

        [HttpGet]
        public IActionResult Get()
        {
            ResponseModel response = new ResponseModel();

            using var controller = new GpioController();
            int pin = 24;
            controller.OpenPin(pin, PinMode.Output);
            if (controller.Read(pin) == PinValue.High)
            {
                controller.Write(pin, PinValue.Low);
                response.OldState = PowerStates.On;
                response.NewState = PowerStates.Off;
            }
            else
            {
                controller.Write(pin, PinValue.High);
                response.OldState = PowerStates.Off;
                response.NewState = PowerStates.On;
            }

            return Ok(new EndpointResponse(true)
            {
                Message = "LED lightning switched successfully.",
                Data = response
            });
        }
    }
}
