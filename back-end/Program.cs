using backend.Configuration;
using backend.Data;
using backend.HostedService;
using backend.Infastructure.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using TestCrudApplication.HostedServices;
using TestCrudApplication.Infrastructure.Connectivity;
using MyApp.Services;
using MyApp.Interfaces;
using MyApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddCorsConfiguration(builder.Configuration);

builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection("Database")
);

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var databaseOptions = serviceProvider
        .GetRequiredService<IOptions<DatabaseOptions>>()
        .Value;

    var connectionStringBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = databaseOptions.Host,
        Port = databaseOptions.Port,
        Database = databaseOptions.Name,
        Username = databaseOptions.UserName,
        Password = databaseOptions.Password
    };

    options.UseNpgsql(connectionStringBuilder.ConnectionString);
});

builder.Services.AddScoped<
    IDatabaseConnectivityChecker,
    DatabaseConnectivityChecker
>();

builder.Services.AddHostedService<StartupConnectionCheckService>();

var app = builder.Build();

app.UseCors("AngularClient");

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();