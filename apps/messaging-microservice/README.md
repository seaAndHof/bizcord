# Messaging Service

Stores the messages people post in Bizcord channels, and publishes `MessagePostedEvent` when a message is posted.

## API

| Method | Route | Success | Errors |
|---|---|---|---|
| POST | `/api/messages` | 201 with the message | 400 if channel, author or content is missing or invalid |
| GET | `/api/messages/{id}` | 200 with the message | 404 |
| GET | `/api/messages?channelId={id}` | 200 with a list (all messages without `channelId`) | |
| PUT | `/api/messages/{id}` | 200 with the edited message | 400 if the content is invalid, 404 |
| DELETE | `/api/messages/{id}` | 204 | 404 |

Request bodies:
- POST: `channelId`, `authorId`, `content`
- PUT: `content`

Content can't be empty or longer than 2,000 characters.

## Events

| Publishes | When |
|---|---|
| `MessagePostedEvent` | After a message has been posted and saved |

## Configuration

| Variable | Meaning | Default |
|---|---|---|
| `RABBITMQ_HOST` | Where RabbitMQ runs | `localhost` |

## Code

- `Api`: the controller and its request types
- `Application`: `MessageService`, the business logic
- `Domain`: `Message` (entity), `MessageContent` (value object), `DomainException`
- `Infrastructure`: the in-memory repository
- `tests`:
  - unit tests for the domain;
  - a contract test that checks every field of the published event;
  - an integration test that posts a message and reads it back.

  The last two run against a real RabbitMQ in Testcontainers.
