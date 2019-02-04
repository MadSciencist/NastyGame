using Api.Common.Infrastructure;
using Api.Common.Messaging.Abstractions;
using Api.Common.Messaging.RabbitMQ;
using Api.Hub.Domain.GameDomain;
using Api.Hub.Domain.Services;
using Api.Hub.Hubs;
using Api.Hub.Infrastructure;
using Api.Hub.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using System;

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
        public IServiceProvider ConfigureServices(IServiceCollection services)
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

            services.AddSingleton<IEventBus, RabbitMQEventBus>(provider =>
            {
                var conn = provider.GetRequiredService<IRabbitMQConnection>();
                var logger = provider.GetRequiredService<ILogger<RabbitMQEventBus>>();
                var scope = provider.GetRequiredService<ILifetimeScope>();
                var subsManager = provider.GetRequiredService<IEventBusSubscriptionManager>();

                return new RabbitMQEventBus(conn, logger, scope, subsManager);
            });

            services.AddSingleton<IEventBusSubscriptionManager, EventBusSubscriptionManager>();

            var autofacContainer = new ContainerBuilder();
            autofacContainer.Populate(services);

            return new AutofacServiceProvider(autofacContainer.Build());
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
