using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceMonitor.TestCommand
{
    public class CommandRequest
    {
        public double? Value { get; set; }
        public string Command { get; set; }
    }
}