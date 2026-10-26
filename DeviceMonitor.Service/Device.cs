using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceMonitor.Service
{
    public class Device
    {
        public Guid DeviceId { get; set; }
        public bool IsRunning {  get; set; }
        public string FirmwareVersion { get; set; } = string.Empty;
        public int IntervalMs { get; set; }
        public DateTime TimestampUTC { get; set; }
        public bool IsAlarm { get; set; }

    }
}
