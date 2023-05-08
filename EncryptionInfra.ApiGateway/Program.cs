using Microsoft.AspNetCore.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Hosting;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Configuration.AddJsonFile("appsettings.json", false, true);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);

builder.Host.UseSerilog((logger, config) =>
{
    config
       .MinimumLevel.Information()
       .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
       .Enrich.FromLogContext()
       .WriteTo.File(@"Logs\log.txt", rollingInterval: RollingInterval.Day);
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

await app.UseOcelot();

app.Run();
