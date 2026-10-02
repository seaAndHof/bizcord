using MessageClient;
using MessagingService.Domain;
using MessagingService.Infrastructure;
using Shared.Contracts.Events;

namespace MessagingService.Application;

public class MessageService(IMessageRepository repository, IMessageClient messageClient)
{
    public async Task<Message> PostAsync(Guid channelId, Guid authorId, string content)
    {
        var message = Message.Create(channelId, authorId, content);
        repository.Save(message);

        await messageClient.PublishAsync(new MessagePostedEvent
        {
            MessageId = message.Id,
            ChannelId = message.ChannelId,
            AuthorId = message.AuthorId,
            Content = message.Content.Value,
            PostedAt = message.PostedAt
        });

        return message;
    }

    public Message? Get(Guid id) => repository.Get(id);

    public IReadOnlyList<Message> List(Guid? channelId) => repository.List(channelId);

    public Message? Edit(Guid id, string content)
    {
        var message = repository.Get(id);
        if (message is null)
            return null;

        message.Edit(content);
        repository.Save(message);
        return message;
    }

    public bool Delete(Guid id) => repository.Delete(id);
}
