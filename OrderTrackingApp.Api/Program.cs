using FluentValidation;
using OrderTrackingApp.Api.MappingProfiles;
using OrderTrackingApp.Api.Validators;
using OrderTrackingApp.Application.Commands.Orders;
using OrderTrackingApp.Persistence.Extensions;
using OrderTrackingApp.ReadPersistence.Extensions;
using OrderTrackingApp.Infrastructure.Extensions;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly));


builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderRequestValidator>();


builder.Services.AddAutoMapper(config => { }, typeof(OrderMappingProfile).Assembly);

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddReadPersistence();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
