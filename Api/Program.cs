using Api;
using Application;
using Infrastructure;
using Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Logging.AddConfiguration(
    configuration.GetSection("Logging")
);
builder.Logging.AddConsole();

builder.Services
    .AddApiServices(configuration)
    .AddInfrastructureServices(configuration)
    .AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.UseApiServices();

app.Run();