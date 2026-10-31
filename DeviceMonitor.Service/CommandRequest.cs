using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceMonitor.Service
{
    public class CommandRequest
    {
        public string Command { get; set; } = string.Empty;
        public double? Value { get; set; } = null;
    }
}
