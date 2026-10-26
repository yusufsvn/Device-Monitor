using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceMonitor.Service
{
    public class TelemetryMessage
    {
        public SensorData? Sensordata { get; set; }
        public Device? DeviceInfo { get; set; }
    }
}
