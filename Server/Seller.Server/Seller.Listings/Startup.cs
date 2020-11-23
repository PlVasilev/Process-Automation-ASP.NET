

namespace Seller.Listings
{
    using Data;
    using Seller.Shared.Infrastructure;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Hangfire;
    using Shared.Messages;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddWebService<ListingsDbContext>(this.Configuration)
                .AddAppServices()
                .AddSwagger()
                .AddMessaging(Configuration)
                .AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Configuration.GetDefaultConnectionString()))
                .AddHangfireServer()
                .AddHostedService<MessagesHostedService>()
                .AddApiControllers();
        

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) =>
            app.UseWebService(env)
                .Initialize();

    }
}
