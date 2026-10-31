using DeviceMonitor.Service;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<Device>();
builder.Services.AddHostedService<CommandListener>();
var host = builder.Build();
host.Run();
