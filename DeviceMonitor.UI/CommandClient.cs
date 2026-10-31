using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace DeviceMonitor.UI
{
    public class CommandClient 
    {

        public Task<CommandResponse> SendAsync(CommandRequest request) => Task.Run(() =>
        {
            using var reqSkt = new RequestSocket();
            reqSkt.Options.Linger = TimeSpan.Zero;

            reqSkt.Connect("tcp://127.0.0.1:5051");
            var data = JsonSerializer.Serialize<CommandRequest>(request);
            reqSkt.SendFrame(data);
            if (!reqSkt.TryReceiveFrameString(TimeSpan.FromSeconds(2), out var reply))
                return new CommandResponse { Success = false, Message = "No response (timeout)" };

            return JsonSerializer.Deserialize<CommandResponse>(reply)
                   ?? new CommandResponse { Success = false, Message = "Invalid response" };
        });
    }
}
