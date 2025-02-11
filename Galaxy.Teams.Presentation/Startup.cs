﻿using Galaxy.Teams.Core.Intefaces;
using Galaxy.Teams.Infrastructure;
using Galaxy.Teams.Presentation.Ioc;
using Galaxy.Teams.Presentation.Middleware;
using Galaxy.Teams.Presentation.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Galaxy.Teams.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseContext(Configuration);
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddServices();
            services.AddGrpc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            app.ConfigureExceptionHandler(logger.CreateLogger("Galaxy.Teams.GlobalLogger"));
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<RobotService>();
                endpoints.MapGrpcService<ShuttleService>();
                endpoints.MapGrpcService<TeamService>();
                endpoints.MapGrpcService<CaptainService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
