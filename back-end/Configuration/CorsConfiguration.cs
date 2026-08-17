namespace backend.Configuration;

public static class CorsConfiguration
{
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var origin = configuration.GetSection("Cors:Origins").Get<string[]>() ?? []; ;
        
        services.AddCors(options =>
            {
                options.AddPolicy("AngularClient", policy =>
                    {
                        policy
                        .WithOrigins(origin)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
        return services;
    }
}