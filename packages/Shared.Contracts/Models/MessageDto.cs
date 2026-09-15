namespace Shared.Contracts.Models;

public record MessageDto(Guid MessageId, Guid ChannelId, Guid AuthorId, string Content, DateTime PostedAt);
