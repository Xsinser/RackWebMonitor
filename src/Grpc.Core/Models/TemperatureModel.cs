using Grpc.Core.Monitor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Core.Models
{
    public class TemperatureModel
    {
        public string Sign { get; set; }
        public float Value { get; set; }
    }
}
