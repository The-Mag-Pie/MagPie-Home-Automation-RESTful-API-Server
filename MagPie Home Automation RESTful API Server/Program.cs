using MagPie_Home_Automation_RESTful_API_Server;
using MagPie_Home_Automation_RESTful_API_Server.BackgroundTasks;
using Microsoft.AspNetCore.HttpOverrides;

// Start background tasks
BackgroundTasksStarter.Start();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Configure API Key Authorization
app.UseMiddleware<ApiKeyMiddleware>();

// Headers from Apache proxy
app.UseForwardedHeaders(new ForwardedHeadersOptions()
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
