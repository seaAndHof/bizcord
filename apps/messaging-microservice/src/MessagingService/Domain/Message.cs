namespace MessagingService.Domain;

public class Message
{
    public Guid Id { get; }
    public Guid ChannelId { get; }
    public Guid AuthorId { get; }
    public MessageContent Content { get; private set; }
    public DateTime PostedAt { get; }
    public DateTime? EditedAt { get; private set; }

    private Message(Guid id, Guid channelId, Guid authorId, MessageContent content, DateTime postedAt)
    {
        Id = id;
        ChannelId = channelId;
        AuthorId = authorId;
        Content = content;
        PostedAt = postedAt;
    }

    public static Message Create(Guid channelId, Guid authorId, string content)
    {
        if (channelId == Guid.Empty)
            throw new DomainException("ChannelId is required.");
        if (authorId == Guid.Empty)
            throw new DomainException("AuthorId is required.");

        return new Message(Guid.NewGuid(), channelId, authorId, new MessageContent(content), DateTime.UtcNow);
    }

    public void Edit(string content)
    {
        Content = new MessageContent(content);
        EditedAt = DateTime.UtcNow;
    }
}
