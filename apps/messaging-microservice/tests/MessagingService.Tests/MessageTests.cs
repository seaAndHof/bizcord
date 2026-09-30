using FluentAssertions;
using MessagingService.Domain;
using Xunit;

namespace MessagingService.Tests;

public class MessageTests
{
    [Fact]
    public void Message_Create_SetsAllFields()
    {
        var channelId = Guid.NewGuid();
        var authorId = Guid.NewGuid();

        var message = Message.Create(channelId, authorId, "Hello world");

        message.Id.Should().NotBe(Guid.Empty);
        message.ChannelId.Should().Be(channelId);
        message.AuthorId.Should().Be(authorId);
        message.Content.Value.Should().Be("Hello world");
        message.PostedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Message_Create_Throws_WhenContentIsEmpty()
    {
        var act = () => Message.Create(Guid.NewGuid(), Guid.NewGuid(), "");

        act.Should().Throw<DomainException>();
    }
}
