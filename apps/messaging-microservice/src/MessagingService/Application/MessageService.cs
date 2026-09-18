using MessagingService.Domain;
using MessagingService.Infrastructure;

namespace MessagingService.Application;

public class MessageService(IMessageRepository repository)
{
    public Message Post(Guid channelId, Guid authorId, string content)
    {
        var message = Message.Create(channelId, authorId, content);
        repository.Save(message);
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
