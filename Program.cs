using BasketService.API;
using BasketService.API.Event;
using BasketService.API.Models;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);
builder.Services.AddSingleton(appSettings);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<UpdateProductConsumer>();


    config.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.UseHealthCheck(provider);
        cfg.Host(appSettings.RabbitMQConnectionString);
        cfg.ReceiveEndpoint("update-item-queue", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<UpdateProductConsumer>(provider);
        });
    }));

    //config.UsingRabbitMq((ctx, cfg) =>
    //{
    //    cfg.Host(appSettings.RabbitMQConnectionString);
    //    cfg.UseHealthCheck(ctx);
    //    cfg.UseMessageRetry(r => r.Immediate(3));
    //    cfg.ReceiveEndpoint("update-item-queue", c => {
    //        c.ConfigureConsumer<UpdateProductConsumer>(ctx);
    //        c.UseMessageRetry(r => r.Immediate(3));
    //    });
    //});
});


builder.Services.AddMassTransitHostedService();

builder.Services.AddAutoMapper(typeof(Program));


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
