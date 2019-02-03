using Microsoft.Extensions.DependencyInjection;

namespace Api.Common.Infrastructure
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddDefaultCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(settings =>
            {
                settings.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            return services;
        }
    }
}
