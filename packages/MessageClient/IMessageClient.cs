namespace MessageClient;

public interface IMessageClient
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;

    Task SubscribeAsync<T>(string subscriptionId, Func<T, CancellationToken, Task> handler,
        CancellationToken cancellationToken = default) where T : class;
}
