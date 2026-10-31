using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DeviceMonitor.UI
{
    public class ViewModel : INotifyPropertyChanged
    {

        // Fields
        private double _temperature;
        private double _pressure;
        private bool _isAlarm;
        private readonly CommandClient _commands;
        private string _commandResult = "";

        // Properties
        public double Temperature { get => _temperature; set { _temperature = value; OnPropertyChanged(); } }
        public double Pressure { get => _pressure; set { _pressure = value; OnPropertyChanged(); } }
        public bool IsAlarm { get => _isAlarm; set { _isAlarm = value; OnPropertyChanged(); } }




        public string CommandResult { get => _commandResult; set { _commandResult = value; OnPropertyChanged(); } }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ViewModel(TelemetryClient client, CommandClient commands) 
        {
            _commands = commands;
            client.MessageReceived += OnMessage;

            StartCommand = new RelayCommand(async _ => await Send(new CommandRequest { Command="start" }));
            StopCommand = new RelayCommand(async _ => await Send(new CommandRequest { Command = "stop" }));
        }

        private async Task Send(CommandRequest request)
        {
            CommandResponse response = await _commands.SendAsync(request);
            CommandResult = response.Message;  
        }
        private void OnMessage(string topic, TelemetryMessage msg)
        {
            System.Diagnostics.Debug.WriteLine($"OnMessage topic={topic}");
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                if (topic == "SensorDataCreator")
                {
                    if (msg.Sensordata != null && msg.DeviceInfo != null)
                    {
                        Temperature = msg.Sensordata.Temperature;
                        Pressure = msg.Sensordata.Pressure;
                        IsAlarm = msg.DeviceInfo.IsAlarm;
                    }

                }

            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
