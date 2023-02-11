using MagPie_Home_Automation_RESTful_API_Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Device.Gpio;

namespace MagPie_Home_Automation_RESTful_API_Server.Controllers.MyRoom.ShelfLed
{
    [Route("room/shelfled/state")]
    [ApiController]
    public class State : ControllerBase
    {
        private class ResponseModel
        {
            public string State { get; set; } = PowerStates.Unknown;
        }

        [HttpGet]
        public IActionResult Get()
        {
            ResponseModel response = new ResponseModel();

            using var controller = new GpioController();
            int pin = 24;
            controller.OpenPin(pin, PinMode.Output);
            if (controller.Read(pin) == PinValue.High)
                response.State = PowerStates.On;
            else
                response.State = PowerStates.Off;

            return Ok(new EndpointResponse(true)
            {
                Data = response
            });
        }
    }
}
