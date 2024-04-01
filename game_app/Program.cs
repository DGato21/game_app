using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Domain.Core.Interfaces;
using Infrastructure.Crosscutting.Settings;
using Infrastructure.Configuration;

/** Gaming Application README

Ideia from Let's Get Checked Challenge

Main Game: TurtleChallenge

March 2024

**/

//TODO: Read files from Arg

Console.WriteLine("Welcome to Gaming Console Application");

Console.WriteLine("Loading all applications");

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.LoadAllGames();

Console.WriteLine("Reading Settings...");

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.Configure<TurtleChallengeSettings>(
    builder.Configuration.GetSection(TurtleChallengeSettings.APP_SETTING_KEY));

using IHost host = builder.Build();

//Get All Services of type Game Running
foreach (IGame service in host.Services.GetServices(typeof(IGame)))
{
    Console.WriteLine($"\nStarting {service.GetType()} [{service.GetInstanceId()}]\n");
    service.Start();
}

await host.RunAsync();