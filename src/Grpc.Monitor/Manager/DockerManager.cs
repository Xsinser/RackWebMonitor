using Grpc.Core.Models;
using Grpc.Monitor.Helper;

namespace Grpc.Monitor.Manager;

public class DockerManager
{
    private CommandHelper _commandHelperInfo;
    private CommandHelper _commandHelperResource;

    public DockerManager()
    {
        _commandHelperInfo = new CommandHelper("docker ps --format json --no-trunc");
        _commandHelperResource = new CommandHelper("docker stats --no-stream --format json");
    }

    public List<DockerContainerInfoModel> GetDockerContainerInfos()
    {
#if DEBUG
        return [
            new() { ID = "1", Command = string.Empty, CreatedAt = string.Empty, Image = string.Empty, Labels = string.Empty, Size = string.Empty, State = string.Empty, Status = string.Empty, LocalVolumes = string.Empty, Mounts = string.Empty, Names = string.Empty, Networks = string.Empty, Platform = string.Empty, Ports = string.Empty, RunningFor = string.Empty },
            new() { ID = "2", Command = string.Empty, CreatedAt = string.Empty, Image = string.Empty, Labels = string.Empty, Size = string.Empty, State = string.Empty, Status = string.Empty, LocalVolumes = string.Empty, Mounts = string.Empty, Names = string.Empty, Networks = string.Empty, Platform = string.Empty, Ports = string.Empty, RunningFor = string.Empty  }];
#else
            var str = _commandHelperInfo.ExecuteCommand();
            return JsonSerializer.Deserialize<List<DockerContainerInfoModel>>(str) ?? [];
#endif
    }

    public async Task<List<DockerContainerInfoModel>> GetDockerContainerInfosAsync()
    {
#if DEBUG
        return [
            new() { ID = "1", Command = string.Empty, CreatedAt = string.Empty, Image = string.Empty, Labels = string.Empty, Size = string.Empty, State = string.Empty, Status = string.Empty, LocalVolumes = string.Empty, Mounts = string.Empty, Names = string.Empty, Networks = string.Empty, Platform = string.Empty, Ports = string.Empty, RunningFor = string.Empty },
            new() { ID = "2", Command = string.Empty, CreatedAt = string.Empty, Image = string.Empty, Labels = string.Empty, Size = string.Empty, State = string.Empty, Status = string.Empty, LocalVolumes = string.Empty, Mounts = string.Empty, Names = string.Empty, Networks = string.Empty, Platform = string.Empty, Ports = string.Empty, RunningFor = string.Empty  }];
#else
            var str = await _commandHelperInfo.ExecuteCommandAsync();
            return JsonSerializer.Deserialize<List<DockerContainerInfoModel>>(str) ?? [];
#endif
    }

    public List<DockerContainerResourceModel> GetDockerContainerResources()
    {
#if DEBUG
        return [
            new() { ID = "1", BlockIO = string.Empty, Container = string.Empty, CPUPerc = string.Empty, MemPerc = string.Empty, MemUsage = string.Empty, Name = string.Empty, NetIO = string.Empty, PIDs = string.Empty },
            new() { ID = "2", BlockIO = string.Empty, Container = string.Empty, CPUPerc = string.Empty, MemPerc = string.Empty, MemUsage = string.Empty, Name = string.Empty, NetIO = string.Empty, PIDs = string.Empty }];
#else
            var str = _commandHelperResource.ExecuteCommand();
            return JsonSerializer.Deserialize<List<DockerContainerResourceModel>>(str) ?? [];
#endif
    }

    public async Task<List<DockerContainerResourceModel>> GetDockerContainerResourcesAsync()
    {
#if DEBUG
        return [
            new() { ID = "1", BlockIO = string.Empty, Container = string.Empty, CPUPerc = string.Empty, MemPerc = string.Empty, MemUsage = string.Empty, Name = string.Empty, NetIO = string.Empty, PIDs = string.Empty },
            new() { ID = "2", BlockIO = string.Empty, Container = string.Empty, CPUPerc = string.Empty, MemPerc = string.Empty, MemUsage = string.Empty, Name = string.Empty, NetIO = string.Empty, PIDs = string.Empty }];
#else
            var str = await _commandHelperResource.ExecuteCommandAsync();
            return JsonSerializer.Deserialize<List<DockerContainerResourceModel>>(str) ?? [];
#endif
    }
}