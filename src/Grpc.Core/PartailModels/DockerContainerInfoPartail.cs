using Grpc.Core.Models;

namespace Grpc.Core.Monitor
{
    public partial class DockerContainerInfoResponse
    {
        public static implicit operator DockerContainerInfoModel(DockerContainerInfoResponse info)
        {
            return new()
            {
                LocalVolumes = info.LocalVolumes,
                ID = info.Id,
                Command = info.Command,
                CreatedAt = info.CreatedAt,
                Image = info.Image,
                Labels = info.Labels,
                Mounts = info.Mounts,
                Names = info.Names,
                Networks = info.Networks,
                Platform = info.Platform,
                Ports = info.Ports,
                RunningFor = info.RunningFor,
                Size = info.Size,
                State = info.State,
                Status = info.Status,
            };
        }

        public static implicit operator DockerContainerInfoResponse(DockerContainerInfoModel info)
        {
            return new()
            {
                LocalVolumes = info.LocalVolumes,
                Id = info.ID,
                Command = info.Command,
                CreatedAt = info.CreatedAt,
                Image = info.Image,
                Labels = info.Labels,
                Mounts = info.Mounts,
                Names = info.Names,
                Networks = info.Networks,
                Platform = info.Platform,
                Ports = info.Ports,
                RunningFor = info.RunningFor,
                Size = info.Size,
                State = info.State,
                Status = info.Status,
            };
        }
    }
}