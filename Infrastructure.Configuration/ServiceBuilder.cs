using Domain.Core;
using Domain.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Crosscutting.Settings;

namespace Infrastructure.Configuration
{
    public static class ServiceBuilder
    {
        public static void LoadAllGames(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGame, TurtleGame>();
        }
    }
}
