using NetMQ;
using NetMQ.Sockets;
using System.Text.Json;

namespace DeviceMonitor.Service
{
    public class Worker(ILogger<Worker> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var publisher = new PublisherSocket();
            publisher.Bind("tcp://127.0.0.1:5050");

            while (!stoppingToken.IsCancellationRequested)
            {

                if (logger.IsEnabled(LogLevel.Information))
                {
                    SensorData data = CreateSensorData();
                    string JsonString = JsonSerializer.Serialize(data);
                    publisher
                        .SendMoreFrame("SensorDataCreator") 
                        .SendFrame(JsonString);
                    logger.LogInformation("Sensor Data created and sended {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }


        private SensorData CreateSensorData()
        {
            SensorData sensorData = new()
            {
                Pressure=CreateRandomFloat(1000f,0f),
                Temperature=CreateRandomFloat(1000f,-273f)
            };

            return sensorData;
 
        }

        private float CreateRandomFloat(float max,float min)
        {
            Random rnd = new();
            float data = (float)(rnd.NextDouble() * (max - min) + min);
            return data;
        }


    }
}
