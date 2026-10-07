# Bizcord: Messaging Service

The backend for posting messages in Bizcord, a Discord-like chat app. Built for System Integration (E2026) at EASV.

| Folder | What it is |
|---|---|
| `apps/messaging-microservice` | The Messaging Service: REST API, domain model and tests |
| `packages/MessageClient` | `IMessageClient`, a small wrapper around EasyNetQ (RabbitMQ) |
| `packages/Shared.Contracts` | What other services can use: the shared model (`MessageDto`) and events (`MessagePostedEvent`) |
| `docs` | [Analysis and design](docs/analysis-and-design.md) |

## Run

```sh
docker compose up --build
```

- API: http://localhost:8000/api/messages
- RabbitMQ management: http://localhost:15672 (guest / guest)

Post a message:

```sh
curl -X POST http://localhost:8000/api/messages \
  -H "Content-Type: application/json" \
  -d '{"channelId": "11111111-1111-1111-1111-111111111111", "authorId": "22222222-2222-2222-2222-222222222222", "content": "Hello world"}'
```

## Test

Docker must be running, because the integration tests start RabbitMQ in a container (Testcontainers).

```sh
dotnet test
```
