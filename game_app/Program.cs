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

/*
 * - Retirar logs
 * - Colocar constraints de tamanho no board?
 * - Rever codigo
 */

Console.WriteLine("Welcome to Gaming Console Application");

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

Console.WriteLine("Reading Settings...");

#region Settings Section

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var defaultSettings = builder.Configuration.GetSection(TurtleChallengeSettings.APP_SETTING_KEY);

string? boardSettings = null, movesSettings = null;
if (args != null && args.Any() && args.Length == 2)
{
    boardSettings = args[0];
    movesSettings = args[1];

    defaultSettings["Configurations:GameSettings"] = boardSettings;
    defaultSettings["Configurations:CommandSettings"] = movesSettings;
}
else
{
    Console.WriteLine("Default settings read from appsettings.json");
}
builder.Services.Configure<TurtleChallengeSettings>(defaultSettings);

#endregion

Console.WriteLine("Loading all applications");
builder.Services.LoadAllGames();

using IHost host = builder.Build();

//Get All Services of type Game Running
foreach (IGame service in host.Services.GetServices(typeof(IGame)))
{
    Console.WriteLine($"\nStarting {service.GetType()} [{service.GetInstanceId()}]\n");
    service.Start();
}

await host.RunAsync();