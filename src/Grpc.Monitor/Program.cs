using Grpc.Monitor.Manager;
using Grpc.Monitor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddTransient<TemperatureManager>();
builder.Services.AddTransient<DockerManager>();

var app = builder.Build();

app.MapGrpcService<InformationService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();