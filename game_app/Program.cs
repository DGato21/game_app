using Microsoft.Extensions.Hosting;
using Infrastructure.Configuration;

/** Gaming Application README

Ideia from Let's Get Checked Challenge

Main Game: TurtleChallenge

March 2024

**/

Console.WriteLine("Welcome to Gaming Console Application");

Console.WriteLine("Loading all applications");

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.LoadAllGames();

IHost host = builder.Build();   

Console.WriteLine("Reading Settings.");

//TODO: Read Settings


//Start the Game

//Do this configurable
Console.WriteLine("Reading by default: TurtleChallenge");

await host.RunAsync();