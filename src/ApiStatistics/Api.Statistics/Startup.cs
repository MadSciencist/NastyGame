using System;
using Api.Common.Infrastructure;
using Api.Common.Messaging.Abstractions;
using Api.Common.Messaging.RabbitMQ;
using Api.Statistics.Domain;
using Api.Statistics.Domain.DTOs;
using Api.Statistics.Domain.Entity;
using Api.Statistics.EventHandlers;
using Api.Statistics.Events;
using Api.Statistics.Infrastructure.Mappings;
using Api.Statistics.Infrastructure.Repository;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

namespace Api.Statistics
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
            services.Configure<ApiStatisticsConfiguration>(Configuration);

            services.AddDefaultCorsPolicy();

            services.AddHealthChecks().AddCheck<HealthCheck>("default");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API docs" });
            });

            services.AddJwthAuthentication(Configuration);
            services.AddAuthorization();

            services.AddAutoMapper(typeof(MappingConfiguration).Assembly); 

            services.AddTransient<IStatisticsRepository, StatisticsRepository>();
            services.AddTransient<UpdateKillsEventHandler>();
            services.AddTransient<UpdateUserDeadthsEventHandler>();
            services.AddTransient<PlayerStartedNewGameEventHandler>();

            services.AddSingleton<IRabbitMQConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RabbitMQConnection>>();
                var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672, UserName = "admin", Password = "admin", VirtualHost = "/" };

                return new RabbitMQConnection(factory, logger);
            });

            services.AddSingleton<IEventBus, RabbitMqEventBus>(provider =>
            {
                var conn = provider.GetRequiredService<IRabbitMQConnection>();
                var logger = provider.GetRequiredService<ILogger<RabbitMqEventBus>>();
                var scope = provider.GetRequiredService<ILifetimeScope>();
                var subsManager = provider.GetRequiredService<IEventBusSubscriptionManager>();

                return new RabbitMqEventBus(conn, logger, scope, subsManager, "Statistics");
            });

            services.AddSingleton<IEventBusSubscriptionManager, EventBusSubscriptionManager>();

            var autofacContainer = new ContainerBuilder();
            autofacContainer.Populate(services);

            return new AutofacServiceProvider(autofacContainer.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //mapper.ConfigurationProvider.AssertConfigurationIsValid();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsPolicy");
            }

            app.UseHealthChecks("/health");
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseAuthentication();

            UseEventBus(app);
        }

        private static void UseEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<UpdateUserDeathsEvent, UpdateUserDeadthsEventHandler>();
            eventBus.Subscribe<UpdateUserKillsEvent, UpdateKillsEventHandler>();
            eventBus.Subscribe<PlayerStartedNewGameEvent, PlayerStartedNewGameEventHandler>();
        }
    }
}
