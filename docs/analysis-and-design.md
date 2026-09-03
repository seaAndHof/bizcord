# Analysis and design

Bizcord is a Discord-like chat app. Each team builds one microservice, which owns one bounded context. This repository is the **Messaging Service**.

## Domains

| Context | Responsibility | Owns |
|---|---|---|
| **Messaging** | Posting, editing, deleting and listing messages in channels | Messages |
| Channels | Creating channels and managing their members | Channels, memberships |
| Users | User profiles | Profiles |
| Notifications | Telling users about mentions and replies | Notifications |

## The Messaging Service

**Responsible for:** posting, editing, deleting and listing messages, and telling other services when a message is posted.

**Not responsible for:** checking that a channel or author exists (Channels and Users own those), or notifying users (Notifications does that).
