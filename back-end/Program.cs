using backend.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCorsConfiguration();

var app = builder.Build();

app.UseCors("AngularClient");

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();