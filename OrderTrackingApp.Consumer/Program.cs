using OrderTrackingApp.Consumer;
using OrderTrackingApp.Persistence.Extensions;
using OrderTrackingApp.ReadPersistence.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddReadPersistence(builder.Configuration);

var host = builder.Build();
host.Run();
