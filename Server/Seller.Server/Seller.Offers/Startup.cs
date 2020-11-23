namespace Seller.Offers
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Data;
    using Infrastructure.Extensions;
    using Messages;
    using Seller.Shared.Infrastructure;
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddWebService<OffersDbContext>(this.Configuration)
                .AddAppServices()
                .AddSwagger()
                .AddMessaging( this.Configuration,
                    typeof(ListingDeletedConsumer),
                    typeof(ListingAcceptedConsumer), 
                    typeof(ListingEditedConsumer))
                .AddApiControllers();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebService(env)
                .Initialize();
        }
    }
}
