// grpc ip http://localhost:5189

using Grpc.Core;
using Grpc.Core.Models;
using Grpc.Core.Monitor;
using Grpc.Net.Client;

await Task.Delay(10_000);

using var channel = GrpcChannel.ForAddress("http://localhost:5189");
var client = new Information.InformationClient(channel);
Console.WriteLine("Создан клиент");
var result1 = (TemperatureModel)await client.GetTemperatureAsync(new(), new());

Console.WriteLine($"{result1.Value}'{result1.Sign}");

Console.WriteLine("Ресурсы контейнера");
await foreach (var item in client.GetDockerContainerResourceStream(new(), new()).ResponseStream.ReadAllAsync())
    Console.WriteLine($"{item.Id}");

Console.WriteLine("Информация контейнера");
await foreach (var item in client.GetDockerContainerInfoStream(new(), new()).ResponseStream.ReadAllAsync())
    Console.WriteLine($"{item.Id}");
Console.ReadLine();