namespace Seller.Listing.Gateway
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Refit;
    using Infrastructure.Extensions;
    using Services;
    using Services.Deal;
    using Seller.Listing.Gateway.Services.Listing;
    using Services.Offer;
    using Seller.Shared.Infrastructure;
    using Seller.Shared.Services.Identity;
    using Microsoft.Extensions.Hosting;
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var serviceEndpoints = this.Configuration
                .GetSection(nameof(ServiceEndpoints))
                .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

            services
                .AddJwtAuthentication(this.Configuration)
                .AddScoped<ICurrentTokenService, CurrentTokenService>()
                .AddTransient<JwtHeaderAuthenticationMiddleware>()
                .AddSwagger()
                .AddApiControllers();

            services
                .AddRefitClient<IListingService>()
                .WithConfiguration(serviceEndpoints.Listing);
            services
                .AddRefitClient<IOfferService>()
                .WithConfiguration(serviceEndpoints.Offer);
            services
                .AddRefitClient<IDealService>()
                .WithConfiguration(serviceEndpoints.Listing);
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app
                .UseJwtHeaderAuthentication()
                .UseSwaggerUI()
                .UseRouting()
                .UseCors(x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader())
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
