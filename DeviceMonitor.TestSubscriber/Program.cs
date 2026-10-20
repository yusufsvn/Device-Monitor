using NetMQ;
using NetMQ.Sockets;

using (var subscriber = new SubscriberSocket())
{
    subscriber.Connect("tcp://127.0.0.1:5050");
    subscriber.Subscribe("SensorDataCreator");

    while (true)
    {
        var topic = subscriber.ReceiveFrameString();
        var message = subscriber.ReceiveFrameString();

        Console.WriteLine("From: {0} Data: {1}", topic, message);
    }
}

