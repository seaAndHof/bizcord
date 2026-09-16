using System.Collections.Concurrent;
using MessagingService.Domain;

namespace MessagingService.Infrastructure;

public class InMemoryMessageRepository : IMessageRepository
{
    private readonly ConcurrentDictionary<Guid, Message> _messages = new();

    public void Save(Message message) => _messages[message.Id] = message;

    public Message? Get(Guid id) => _messages.GetValueOrDefault(id);

    public IReadOnlyList<Message> List(Guid? channelId) =>
        _messages.Values
            .Where(m => channelId is null || m.ChannelId == channelId)
            .OrderBy(m => m.PostedAt)
            .ToList();

    public bool Delete(Guid id) => _messages.TryRemove(id, out _);
}
