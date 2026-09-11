using MessageClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMessageClient("host=localhost");

var app = builder.Build();

app.Run();
