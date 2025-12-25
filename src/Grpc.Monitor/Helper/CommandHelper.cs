using System.Diagnostics;

namespace Grpc.Monitor.Helper;

public class CommandHelper : IDisposable
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