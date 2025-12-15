using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;

var tempRaspberyPiCommand = new CommandHelper("vcgencmd measure_temp");
var tempRaspberyPiCommandResult1 = tempRaspberyPiCommand.ExecuteCommand(); //temp=46.6'C <-- нужен парсер. пример строки результата
var tempRaspberyPiCommandResult2 = tempRaspberyPiCommand.ExecuteCommand(); //temp=46.6'C

Console.WriteLine(tempRaspberyPiCommandResult1);
Console.WriteLine(tempRaspberyPiCommandResult2);

public class Temp
{
    public string Literal { get; set; }
    public decimal Value { get; set; }
}

internal class DockerContainerInfo
{
    public string Command { get; set; }
    public string CreatedAt { get; set; }
    public string ID { get; set; }
    public string Image { get; set; }
    public string Labels { get; set; }
    public string LocalVolumes { get; set; }
    public string Mounts { get; set; }
    public string Names { get; set; }
    public string Networks { get; set; }
    public object Platform { get; set; }
    public string Ports { get; set; }
    public string RunningFor { get; set; }
    public string Size { get; set; }
    public string State { get; set; }
    public string Status { get; set; }
}

public class DockerContainerResource
{
    public string BlockIO { get; set; }
    public string CPUPerc { get; set; }
    public string Container { get; set; }
    public string ID { get; set; }
    public string MemPerc { get; set; }
    public string MemUsage { get; set; }
    public string Name { get; set; }
    public string NetIO { get; set; }
    public string PIDs { get; set; }
}

internal class DockerHelper
{
    private CommandHelper _commandHelperInfo;
    private CommandHelper _commandHelperResource;

    public DockerHelper()
    {
        //_commandHelper = new CommandHelper("docker ps --format '{{json .}}' --no-trunc");
        _commandHelperInfo = new CommandHelper("docker ps --format json --no-trunc");
        _commandHelperResource = new CommandHelper("docker stats --no-stream --format json");
    }

    public List<DockerContainerInfo> GetDockerContainerInfos()
    {
        var str = _commandHelperInfo.ExecuteCommand();
        return JsonSerializer.Deserialize<List<DockerContainerInfo>>(str) ?? [];
    }

    public async Task<List<DockerContainerInfo>> GetDockerContainerInfosAsync()
    {
        var str = await _commandHelperInfo.ExecuteCommandAsync();
        return JsonSerializer.Deserialize<List<DockerContainerInfo>>(str) ?? [];
    }

    public List<DockerContainerResource> GetDockerContainerResources()
    {
        var str = _commandHelperResource.ExecuteCommand();
        return JsonSerializer.Deserialize<List<DockerContainerResource>>(str) ?? [];
    }

    public async Task<List<DockerContainerResource>> GetDockerContainerResourcesAsync()
    {
        var str = await _commandHelperResource.ExecuteCommandAsync();
        return JsonSerializer.Deserialize<List<DockerContainerResource>>(str) ?? [];
    }
}

internal class TempHelper
{
    private CommandHelper _commandHelper;

    public TempHelper()
    {
        _commandHelper = new CommandHelper("vcgencmd measure_temp");
    }

    public Temp GetTemp()
    {
        var str = _commandHelper.ExecuteCommand()?.Replace("temp=", "").Split('\'');
        return new() { Value = decimal.Parse(str.First()), Literal = str.Last() };
    }

    public async Task<Temp> GetTempAsync()
    {
        var str = (await _commandHelper.ExecuteCommandAsync())?.Replace("temp=", "").Split('\'');
        return new() { Value = decimal.Parse(str.First()), Literal = str.Last() };
    }
}

internal class CommandHelper : IDisposable
{
    private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    private bool _disposed = false;
    private Process _process;

    public CommandHelper(string command)
    {
        _process = new Process();
        _process.StartInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"{command}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
    }

    public async Task<string> ExecuteCommandAsync()
    {
        await _semaphore.WaitAsync();
        _process.Start();

        string result = _process.StandardOutput.ReadToEnd();

        await _process.WaitForExitAsync();

        _process.Close();
        _semaphore.Release();

        return result;
    }

    public string ExecuteCommand()
    {
        _semaphore.Wait();
        _process.Start();

        string result = _process.StandardOutput.ReadToEnd();

        _process.WaitForExit();

        _process.Close();
        _semaphore.Release();

        return result;
    }

    public void Dispose()
    {
        _process.Dispose();
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _process.Dispose();
        }
        _disposed = true;
    }

    ~CommandHelper()
    {
        Dispose(false);
    }
}