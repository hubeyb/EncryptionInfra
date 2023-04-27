using EncryptionInfra.Business;
using EncryptionInfra.Business.Observers;
using EncryptionInfra.Business.Services;
using EncryptionInfra.Domain.Interfaces;
using EncryptionInfra.Domain.Model;
using EncryptionInfra.Domain.Model.ResponseModel;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis-18543.c250.eu-central-1-1.ec2.cloud.redislabs.com:18543,abortConnect=false,user=default,password=CdC7iEyqoAwITxauAFmjignhEK63R0Rh";
});

builder.Services.AddScoped<EncryptionProducerService>();


//RabbitMQ
//TODO: RabbitMq'settings should be get from appsettings
builder.Services.AddSingleton<ISendObserver, SendObserver>();
builder.Services.AddSingleton<IPublishObserver, PublishObserver>();

builder.Services.AddMassTransit(busRegistrationConfigurator =>
{
    busRegistrationConfigurator.SetKebabCaseEndpointNameFormatter();
    busRegistrationConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
    {
        busFactoryConfigurator.Host("rabbitmq://localhost:5672/", hostConfigurator =>
        {
            hostConfigurator.Username("guest");
            hostConfigurator.Password("guest");
        });
        busFactoryConfigurator.ConnectSendObserver(context.GetRequiredService<ISendObserver>());
        busFactoryConfigurator.ConnectPublishObserver(context.GetRequiredService<IPublishObserver>());
    });

    EndpointConvention.Map<EncryptionText>(new Uri($"queue:encryptioninfra-encryption-text"));
    EndpointConvention.Map<DecryptionText>(new Uri($"queue:encryptioninfra-decryption-text"));
    EndpointConvention.Map<MessageQueueResponse>(new Uri($"queue:encryptioninfra-message-queue-response"));
});

builder.Services.AddScoped(typeof(IMessageQueueProducer<>), typeof(MessageQueueProducer<>));

//RabbitMQ

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
