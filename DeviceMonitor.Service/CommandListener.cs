using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace DeviceMonitor.Service
{
    public class CommandListener : BackgroundService
    {

        private readonly Device _state;
        private readonly ILogger<CommandListener> _logger;
        public CommandListener(Device state, ILogger<CommandListener> logger)
        {
            _state = state;
            _logger = logger;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => Listen(stoppingToken), stoppingToken);
        }



        private void Listen(CancellationToken token)
        {
            using var server = new ResponseSocket();
            server.Bind("tcp://127.0.0.1:5051");

            while (!token.IsCancellationRequested)
            {
                if (!server.TryReceiveFrameString(TimeSpan.FromMilliseconds(500), out var request))
                    continue;

                var response = Handle(request);
                server.SendFrame(JsonSerializer.Serialize(response));
            }


        }

        private CommandResponse Handle(string json)
        {
            CommandRequest req = JsonSerializer.Deserialize<CommandRequest>(json);

            if (req?.Command is null)
                return new CommandResponse { Success = false, Message = "Missing Command" };
            _logger.LogInformation("Command received: {cmd} {val}", req.Command, req.Value);

            switch (req.Command.ToLowerInvariant())
            {
                case "start":
                    _state.IsRunning = true;
                    return new CommandResponse { Success = true, Message = "Started" };
                case "stop":
                    _state.IsRunning = false;
                    return new CommandResponse { Success = true, Message = "Stopped" };
                case "set-interval":
                    if (req.Value > 10000 || req.Value < 100)
                        return new CommandResponse { Success = false, Message = "Interval must be between 100 and 10000 ms" };
                    _state.IntervalMs = (int)req.Value;
                    return new CommandResponse { Success = true, Message = $"Interval set to {(int)req.Value} ms" };
                case "get-status":
                    return new CommandResponse
                    {
                        Success = true,
                        Message = $"running={_state.IsRunning}, interval={_state.IntervalMs}, threshold={_state.Threshold}"
                    };

                default:
                    return new CommandResponse { Success = false, Message = "Unknown Command" };
            }
        }
    }
}
