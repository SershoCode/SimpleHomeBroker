using System.IO;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleHomeBroker.Application.CommandHandlers.HomePC;
using SimpleHomeBroker.EndpointClients;
using SimpleHomeBroker.EndpointClients.HomePC;
using SimpleHomeBroker.Host.Alice.Options;
using SimpleHomeBroker.Host.Alice.Services;
using SimpleHomeBroker.Host.Telegram.Options;
using SimpleHomeBroker.Host.Telegram.Services;

namespace SimpleHomeBroker.Host
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; set; }

        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(
                    $"appsettings{(env.EnvironmentName == "Development" ? ".Development" : string.Empty)}.json", true,
                    true);

            AppConfiguration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(MonitorsOffCommandHandler).Assembly);
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson();

            // Configure Services. 
            services.AddHostedService<TelegramRunnerService>();
            services.AddSingleton<ITelegramBotService, TelegramBotService>();
            services.AddScoped<ITelegramRequestService, TelegramRequestService>();
            services.AddScoped<IAliceRequestService, AliceRequestService>();

            // Configure Endpoint clients.
            services.AddHttpClient<HomePcClient>();
            services.AddScoped<IHomePcClient, HomePcClient>();

            // Configure Options.
            services.Configure<AliceOptions>(AppConfiguration.GetSection(nameof(AliceOptions)));
            services.Configure<TelegramOptions>(AppConfiguration.GetSection(nameof(TelegramOptions)));
            services.Configure<EndpointsOptions>(AppConfiguration.GetSection(nameof(EndpointsOptions)));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}