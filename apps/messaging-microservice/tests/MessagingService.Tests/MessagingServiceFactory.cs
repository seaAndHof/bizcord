using MessageClient;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.RabbitMq;
using Xunit;

namespace MessagingService.Tests;

public class MessagingServiceFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly RabbitMqContainer _rabbitMq = new RabbitMqBuilder("rabbitmq:3-management")
        .WithUsername("guest")
        .WithPassword("guest")
        .Build();

    public IMessageClient MessageClient => Services.GetRequiredService<IMessageClient>();

    public async Task InitializeAsync()
    {
        await _rabbitMq.StartAsync();
        Environment.SetEnvironmentVariable("RABBITMQ_HOST", $"{_rabbitMq.Hostname}:{_rabbitMq.GetMappedPublicPort(5672)}");
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await DisposeAsync();
        await _rabbitMq.DisposeAsync();
    }
}
