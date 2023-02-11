using MagPie_Home_Automation_RESTful_API_Server.Models;

namespace MagPie_Home_Automation_RESTful_API_Server
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEY = "XApiKey"; // TODO

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            if (context.Request.Method == HttpMethods.Options)
            {
                context.Response.StatusCode = 204;
                context.Response.Headers.Add("Access-Control-Allow-Headers", "X-Api-Key");
                return;
            }

            if (context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey) == false)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/problem+json; charset=utf-8";
                var response = new EndpointResponse(false)
                {
                    Message = "API Key not found"
                };
                await context.Response.WriteAsync(response.ToString());
                return;
            }

            //var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            //var apiKey = appSettings.GetValue<string>(APIKEY);
            //if (!apiKey.Equals(extractedApiKey))
            //{
            //    context.Response.StatusCode = 401;
            //    await context.Response.WriteAsync("Unauthorized client");
            //    return;
            //}

            if (APIKEY != extractedApiKey)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/problem+json; charset=utf-8";
                var response = new EndpointResponse(false)
                {
                    Message = "Wrong API Key"
                };
                await context.Response.WriteAsync(response.ToString());
                return;
            }

            await _next(context);
        }
    }
}
