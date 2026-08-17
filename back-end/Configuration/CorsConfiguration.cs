namespace backend.Configuration;

public static class CorsConfiguration
{
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var origin = configuration.GetSection("Cors:Origins").Get<string[]>() ?? [];;
        foreach(string values in origin)
        {
            Console.WriteLine(values);
        }
        services.AddCors(options =>
            {
                options.AddPolicy("AngularClient", policy =>
                    {
                        policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
        return services;
    }
}