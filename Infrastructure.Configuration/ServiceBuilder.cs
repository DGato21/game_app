using Domain.Core;
using Domain.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration
{
    public static class ServiceBuilder
    {
        public static void LoadAllGames(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITurtleGame, TurtleGame>();
        }
    }
}
