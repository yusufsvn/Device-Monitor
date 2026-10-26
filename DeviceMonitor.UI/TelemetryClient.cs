using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.Json;

namespace DeviceMonitor.UI
{
    public class TelemetryClient : IDisposable
    {

        public readonly string topic = "SensorDataCreator";
        private readonly CancellationTokenSource _cts = new();
        public event Action<string, TelemetryMessage>? MessageReceived;
        public void Dispose()
        {
            _cts.Cancel();
        }

        public void Start() 
        {
            Task.Run(() =>
            {
                using var subscriber = new SubscriberSocket();
                subscriber.Connect("tcp://127.0.0.1:5050");
                subscriber.Subscribe("SensorDataCreator");


                while (!_cts.IsCancellationRequested)
                {
                    if (!subscriber.TryReceiveFrameString(TimeSpan.FromMilliseconds(500), out var topic))
                        continue;
                    var response = subscriber.ReceiveFrameString();

                    try
                    {
                        Debug.WriteLine(response);
                        TelemetryMessage message = JsonSerializer.Deserialize<TelemetryMessage>(response);
                        if (message is not null)
                            MessageReceived?.Invoke(topic!, message);
                    }
                    catch (Exception ex) { Debug.WriteLine($"JSON ERROR: {ex}"); }

                }
            });



        }
    }
}
