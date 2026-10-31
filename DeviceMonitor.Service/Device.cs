using DeviceMonitor.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceMonitor.Service
{
    public class Device
    {
        public Guid DeviceId { get; set; } = Guid.NewGuid();
        public bool IsRunning {  get; set; } = false;
        public string FirmwareVersion { get; set; } = "1.2.44.7";
        public int IntervalMs { get; set; } = 1000;
        public DateTime TimestampUTC { get; set; } = DateTime.UtcNow;
        public bool IsAlarm { get; set; } = false;
        public double Threshold { get; set; } = 30;
    }
}
