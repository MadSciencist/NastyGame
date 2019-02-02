using Api.Common.Infrastructure;
using Api.Hub.Domain.Services;
using Api.Hub.Hubs;
using Api.Hub.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using Api.Hub.Domain.GameDomain;

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

            services.AddAuthorization();

            var jwtKey = Configuration["AuthenticationJwt:Key"];
            var jwtIssuer = Configuration["AuthenticationJwt:Issuer"];
            var jwtAudience = Configuration["AuthenticationJwt:Audience"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                        ClockSkew = TimeSpan.FromMinutes(0),
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = OnMessageReceived
                    };
                });

            services.AddHealthChecks().AddCheck<HealthCheck>("default");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSignalR(settings => { settings.EnableDetailedErrors = true; }).AddMessagePackProtocol();

            services.AddSingleton<IUserIdProvider, UserIdProvider>();
            services.AddSingleton<IPlayersService, PlayersService>();
            services.AddSingleton<INotifierTask, PlayerNotifierTask>();
            services.AddTransient<INpcService, NpcService>();
            services.AddSingleton<IGameplay, Gameplay>();
        }

        private Task OnMessageReceived(MessageReceivedContext context)
        {
            var accessToken = context.Request.Query["access_token"];
            //var accessToken = context.Request.Headers["Authorization"].ToString().Split(" ")[1];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/game/socket")))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }

            return Task.CompletedTask;
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
