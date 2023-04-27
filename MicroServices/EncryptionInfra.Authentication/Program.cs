using EncryptionInfra.Business.Services;
using EncryptionInfra.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
//builder.Services.AddScoped<ICache, RedisCacheService>();

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = "redis-18543.c250.eu-central-1-1.ec2.cloud.redislabs.com:18543,abortConnect=false,user=default,password=CdC7iEyqoAwITxauAFmjignhEK63R0Rh";
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

