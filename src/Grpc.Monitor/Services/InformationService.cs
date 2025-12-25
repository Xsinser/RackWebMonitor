using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Core.Models;
using Grpc.Core.Monitor;
using Grpc.Monitor.Manager;

namespace Grpc.Monitor.Services
{
    public class InformationService(ILogger<InformationService> logger, TemperatureManager temperatureManager) : Information.InformationBase
    {
        public override async Task<TemperatureResponse> GetTemperature(Empty request, ServerCallContext context)
        {
            return await temperatureManager.GetTempAsync();
        }
    }
}
