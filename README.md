# DeviceMonitor

A device-monitoring system built with C# and .NET. A background Worker Service
simulates a temperature and pressure sensor and publishes the readings over
ZeroMQ (NetMQ). A WPF dashboard built with MVVM shows the live values and
sends commands back to the service.

## Features
- Live temperature and pressure readings (PUB/SUB)
- Threshold alarm shown on the dashboard
- Start / stop the data stream from the UI (REQ/REP)
- Change the alarm threshold at runtime
- UI and service run as separate processes and share message models
  through a Contracts library

## Tech stack
C#, .NET, WPF (MVVM), Worker Service, NetMQ (ZeroMQ), System.Text.Json

## Project structure
- `DeviceMonitor.Service`: Worker Service, publishes data and handles commands
- `DeviceMonitor.UI`: WPF dashboard
- `DeviceMonitor.Contracts`: shared message models
- `DeviceMonitor.TestSubscriber`: console tool for testing the service

## Photos
<img width="983" height="547" alt="resim" src="https://github.com/user-attachments/assets/94f202d5-e0a6-4da8-8f24-349a9120e933" />
<img width="973" height="547" alt="resim" src="https://github.com/user-attachments/assets/25d72437-313c-49cd-9086-814aefba9dac" />
<img width="977" height="555" alt="resim" src="https://github.com/user-attachments/assets/c8ceab46-04aa-43ec-9a48-32ed1931ee28" />
