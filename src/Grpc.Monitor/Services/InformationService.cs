using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Core.Monitor;
using Grpc.Monitor.Manager;

namespace Grpc.Monitor.Services
{
    public class InformationService(ILogger<InformationService> Logger,
                                    TemperatureManager temperatureManager,
                                    DockerManager DockerManager) : Information.InformationBase
    {
        public override async Task<TemperatureResponse> GetTemperature(Empty request, ServerCallContext context)
        {
            return await temperatureManager.GetTempAsync();
        }

        public override async Task GetDockerContainerInfoStream(Empty request, IServerStreamWriter<DockerContainerInfoResponse> responseStream, ServerCallContext context)
        {
            var data = await DockerManager.GetDockerContainerInfosAsync();
            foreach (var info in data)
                await responseStream.WriteAsync(info);
        }

        public override async Task GetDockerContainerResourceStream(Empty request, IServerStreamWriter<DockerContainerResourceResponse> responseStream, ServerCallContext context)
        {
            var data = await DockerManager.GetDockerContainerResourcesAsync();
            foreach (var info in data)
                await responseStream.WriteAsync(info);
        }
    }
}