using MessagingService.Domain;

namespace MessagingService.Infrastructure;

public interface IMessageRepository
{
    void Save(Message message);
    Message? Get(Guid id);
    IReadOnlyList<Message> List(Guid? channelId);
    bool Delete(Guid id);
}
