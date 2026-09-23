using MessageClient;
using MessagingService.Application;
using MessagingService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMessageClient($"host={builder.Configuration["RABBITMQ_HOST"] ?? "localhost"}");
builder.Services.AddSingleton<IMessageRepository, InMemoryMessageRepository>();
builder.Services.AddScoped<MessageService>();

var app = builder.Build();

app.MapControllers();

app.Run();
