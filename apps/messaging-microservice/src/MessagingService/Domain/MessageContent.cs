namespace MessagingService.Domain;

public record MessageContent
{
    public const int MaxLength = 2000;

    public string Value { get; }

    public MessageContent(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Content can't be empty.");
        if (value.Length > MaxLength)
            throw new DomainException($"Content can't be longer than {MaxLength} characters.");

        Value = value;
    }
}
