using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceMonitor.UI
{
    public class TelemetryMessage
    {
        public SensorData? Sensordata { get; set; }
        public Device? DeviceInfo { get; set; }
    }


    public class Device
    {
        public Guid DeviceId { get; set; }
        public string FirmwareVersion { get; set; } = string.Empty;
        public int Intervalms { get; set; }
        public DateTime TimestampUTC { get; set; }
        public bool IsRunning { get; set; }
        public bool  IsAlarm { get; set; }

    }


    public class SensorData
    {
        public double Temperature { get; set; }
        public double Pressure { get; set; }

    }
}
