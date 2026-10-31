using System.Diagnostics;
using System.Text.Json;
using DeviceMonitor.TestCommand;
using NetMQ;
using NetMQ.Sockets;

using var req = new RequestSocket();
req.Connect("tcp://127.0.0.1:5051");
req.SendFrame(JsonSerializer.Serialize(new CommandRequest { Command = "Start" ,Value=1}));
Console.WriteLine("Aluuuuuuuuuuuuu");
Debug.WriteLine(req.ReceiveFrameString());





