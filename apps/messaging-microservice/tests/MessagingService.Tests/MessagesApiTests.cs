using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Shared.Contracts.Events;
using Shared.Contracts.Models;
using Xunit;

namespace MessagingService.Tests;

public class MessagesApiTests(MessagingServiceFactory factory) : IClassFixture<MessagingServiceFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task PostMessage_PublishedEvent_MeetsContract()
    {
        var channelId = Guid.NewGuid();
        var authorId = Guid.NewGuid();
        var capture = await MessageCapture<MessagePostedEvent>.StartAsync(factory.MessageClient);

        var response = await _client.PostAsJsonAsync("/api/messages", new
        {
            ChannelId = channelId,
            AuthorId = authorId,
            Content = "Hello world"
        });

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var evt = await capture.WaitForMessageAsync(TimeSpan.FromSeconds(5));

        evt.MessageId.Should().NotBe(Guid.Empty);
        evt.ChannelId.Should().Be(channelId);
        evt.AuthorId.Should().Be(authorId);
        evt.Content.Should().Be("Hello world");
        evt.PostedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task PostMessage_Returns201_AndPersistsMessage()
    {
        var response = await _client.PostAsJsonAsync("/api/messages", new
        {
            ChannelId = Guid.NewGuid(),
            AuthorId = Guid.NewGuid(),
            Content = "Hello world"
        });

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await response.Content.ReadFromJsonAsync<MessageDto>();
        body.Should().NotBeNull();
        body!.MessageId.Should().NotBe(Guid.Empty);

        var get = await _client.GetAsync($"/api/messages/{body.MessageId}");
        get.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
