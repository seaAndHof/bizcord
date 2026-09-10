using EasyNetQ;

namespace MessageClient;

public class EasyNetQMessageClient(IBus bus) : IMessageClient
{
    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class =>
        bus.PubSub.PublishAsync(message, cancellationToken);

    public Task SubscribeAsync<T>(string subscriptionId, Func<T, CancellationToken, Task> handler,
        CancellationToken cancellationToken = default) where T : class =>
        bus.PubSub.SubscribeAsync(subscriptionId, handler, _ => { }, cancellationToken);
}
