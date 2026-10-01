using MessageClient;

namespace MessagingService.Tests;

public class MessageCapture<T> where T : class
{
    private readonly TaskCompletionSource<T> _received = new(TaskCreationOptions.RunContinuationsAsynchronously);

    public static async Task<MessageCapture<T>> StartAsync(IMessageClient client)
    {
        var capture = new MessageCapture<T>();
        await client.SubscribeAsync<T>($"test-{Guid.NewGuid()}", (message, _) =>
        {
            capture._received.TrySetResult(message);
            return Task.CompletedTask;
        });
        return capture;
    }

    public Task<T> WaitForMessageAsync(TimeSpan timeout) => _received.Task.WaitAsync(timeout);
}
