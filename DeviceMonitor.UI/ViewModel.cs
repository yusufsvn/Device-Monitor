using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace DeviceMonitor.UI
{
    public class ViewModel : INotifyPropertyChanged
    {

        // Fields
        private double _temperature;
        private double _pressure;
        private bool _isAlarm;


        // Properties
        public double Temperature { get => _temperature; set { _temperature = value; OnPropertyChanged(); } }
        public double Pressure { get => _pressure; set { _pressure = value; OnPropertyChanged(); } }
        public bool IsAlarm { get => _isAlarm; set { _isAlarm = value; OnPropertyChanged(); } }



        public ViewModel(TelemetryClient client) 
        {
            client.MessageReceived += OnMessage;
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
