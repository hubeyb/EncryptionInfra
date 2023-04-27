
using EncryptionInfra.Business.Consumers;
using EncryptionInfra.Business.Observers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Reflection;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

IHost host = Host.CreateDefaultBuilder(args)
                 .Build();

//RabbitMQ
//TODO: RabbitMq'settings should be get from appsettings

builder.Services.AddSingleton<IConsumeObserver, ConsumeObserver>();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddConsumeObserver<ConsumeObserver>();
    busConfigurator.AddConsumer<EncryptionConsumer>();
    busConfigurator.AddConsumer<DecryptionConsumer>();
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
    {
        busFactoryConfigurator.Host("rabbitmq://localhost:5672/", hostConfigurator =>
        {
            hostConfigurator.Username("guest");
            hostConfigurator.Password("guest");
        });

        busFactoryConfigurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("encryptioninfra-", false));
    });
});


await host.RunAsync();