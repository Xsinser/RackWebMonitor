using Grpc.Core.Models;

namespace Grpc.Core.Monitor
{
    public partial class DockerContainerResourceResponse
    {
        public static implicit operator DockerContainerResourceModel(DockerContainerResourceResponse resource)
        {
            return new()
            {
                ID = resource.Id,
                PIDs = resource.Pids,
                BlockIO = resource.BlockIo,
                Container = resource.Container,
                CPUPerc = resource.CpuPerc,
                MemPerc = resource.MemPerc,
                MemUsage = resource.MemUsage,
                Name = resource.Name,
                NetIO = resource.NetIo
            };
        }

        public static implicit operator DockerContainerResourceResponse(DockerContainerResourceModel resource)
        {
            return new()
            {
                Id = resource.ID,
                Pids = resource.PIDs,
                BlockIo = resource.BlockIO,
                Container = resource.Container,
                CpuPerc = resource.CPUPerc,
                MemPerc = resource.MemPerc,
                MemUsage = resource.MemUsage,
                Name = resource.Name,
                NetIo = resource.NetIO
            };
        }
    }
}