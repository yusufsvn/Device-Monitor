using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceMonitor.UI
{
    public class CommandRequest
    {
        public string Command { get; set; }
        public double Value { get; set; }
    }


    public class CommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
