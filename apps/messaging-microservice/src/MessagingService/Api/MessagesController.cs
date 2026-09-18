using MessagingService.Application;
using MessagingService.Domain;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Models;

namespace MessagingService.Api;

public record CreateMessageRequest(Guid ChannelId, Guid AuthorId, string Content);

public record EditMessageRequest(string Content);

[ApiController]
[Route("api/messages")]
public class MessagesController(MessageService messages) : ControllerBase
{
    [HttpPost]
    public IActionResult Post(CreateMessageRequest request)
    {
        try
        {
            var message = messages.Post(request.ChannelId, request.AuthorId, request.Content);
            return CreatedAtAction(nameof(Get), new { id = message.Id }, ToDto(message));
        }
        catch (DomainException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id) =>
        messages.Get(id) is { } message ? Ok(ToDto(message)) : NotFound();

    [HttpGet]
    public IEnumerable<MessageDto> List(Guid? channelId) =>
        messages.List(channelId).Select(ToDto);

    [HttpPut("{id:guid}")]
    public IActionResult Edit(Guid id, EditMessageRequest request)
    {
        try
        {
            return messages.Edit(id, request.Content) is { } message ? Ok(ToDto(message)) : NotFound();
        }
        catch (DomainException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) =>
        messages.Delete(id) ? NoContent() : NotFound();

    private static MessageDto ToDto(Message message) =>
        new(message.Id, message.ChannelId, message.AuthorId, message.Content.Value, message.PostedAt);
}
