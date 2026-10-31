using System.Diagnostics;
using System.Text.Json;
using Device.TestCommand;
using NetMQ;
using NetMQ.Sockets;

using var req = new RequestSocket();
req.Connect("tcp://127.0.0.1:5051");
req.SendFrame(JsonSerializer.Serialize(new CommandRequest { Command = "Start"}));
Debug.WriteLine(req.ReceiveFrameString());


using System;
using System.Collections.Generic;
using System.Text;

namespace Device.TestCommand
{
    public class CommandRequest
    {
        public double? Value { get; set; }
        public string Command { get; set; }
    }
}


