// grpc ip http://localhost:5189

using Grpc.Core.Models;
using Grpc.Core.Monitor;
using Grpc.Net.Client;

await Task.Delay(10_000);

using var channel = GrpcChannel.ForAddress("http://localhost:5189");
var client = new Information.InformationClient(channel);
Console.WriteLine("Создан клиент");
var result = (TemperatureModel)await client.GetTemperatureAsync(new(), new());

Console.WriteLine($"{result.Value}'{result.Sign}");
Console.ReadLine();