using System.Text.Json;

namespace MagPie_Home_Automation_RESTful_API_Server.Models
{
    public class EndpointResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; } = null;
        public object? Data { get; set; } = null;

        public EndpointResponse(bool success)
        {
            Success = success;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
