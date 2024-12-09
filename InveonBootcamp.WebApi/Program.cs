using InveonBootcamp.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<BookService>();

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse("localhost:6379", true);
    configuration.AllowAdmin = true;
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddSingleton<RedisService>();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
