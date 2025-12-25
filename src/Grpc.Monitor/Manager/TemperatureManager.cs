using Grpc.Core.Models;
using Grpc.Monitor.Helper;

namespace Grpc.Monitor.Manager
{
    public class TemperatureManager
    {
        private CommandHelper _commandHelper;

        public TemperatureManager()
        {
            _commandHelper = new CommandHelper("vcgencmd measure_temp");
        }

        public TemperatureModel GetTemp()
        {
#if DEBUG
            return new() { Value = 46.1f, Sign = "C" };
#else
            var str = _commandHelper.ExecuteCommand()?.Replace("temp=", "").Split('\'');
            return new() { Value = float.Parse(str.First()), Sign = str.Last() };
#endif
        }

        public async Task<TemperatureModel> GetTempAsync()
        {
#if DEBUG
            return new() { Value = 46.1f, Sign = "C" };
#else
            var str = (await _commandHelper.ExecuteCommandAsync())?.Replace("temp=", "").Split('\'');
            return new() { Value = float.Parse(str.First()), Sign = str.Last() };
#endif
        }
    }
}