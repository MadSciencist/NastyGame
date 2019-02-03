using Api.Common.Infrastructure;
using Api.Hub.Domain.GameDomain;
using Api.Hub.Domain.Services;
using Api.Hub.Hubs;
using Api.Hub.Infrastructure;
using Api.Hub.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;

namespace Api.Hub
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultCorsPolicy();

            services.AddJwthAuthentication(Configuration);
            services.AddAuthorization();

            services.AddHealthChecks().AddCheck<HealthCheck>("default");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSignalR(settings => { settings.EnableDetailedErrors = true; }).AddMessagePackProtocol();

            services.AddScoped<IHubTokenHandler, HubTokenHandler>();
            services.AddSingleton<IPlayersService, PlayersService>();
            services.AddSingleton<INotifierTask, PlayerNotifierTask>();
            services.AddTransient<INpcService, NpcService>();
            services.AddSingleton<IGameplay, Gameplay>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("CorsPolicy");
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }

            app.UseHealthChecks("/health");
            app.UseMvc();
            app.UseSignalR(routes =>
            {
                routes.MapHub<GameHub>("/game/socket");
            });

            app.UseAuthentication();
        }
    }
}
