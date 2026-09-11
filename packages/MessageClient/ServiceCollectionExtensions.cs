using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

namespace MessageClient;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessageClient(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton(_ => RabbitHutch.CreateBus(connectionString));
        services.AddSingleton<IMessageClient, EasyNetQMessageClient>();
        return services;
    }
}
