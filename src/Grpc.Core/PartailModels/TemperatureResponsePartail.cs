using Grpc.Core.Models;
using Grpc.Core.Monitor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Core.Monitor
{
    public partial class TemperatureResponse
    {
        public static implicit operator TemperatureModel(TemperatureResponse temperature)
        {
            return new() { Value = temperature.Value, Sign = temperature.Sign };
        }

        public static implicit operator TemperatureResponse(TemperatureModel temperature)
        {
            return new() { Value = temperature.Value, Sign = temperature.Sign };
        }
    }
}
