namespace Seller.Listings.Infrastructure.Extensions
{
    using Features.Listing.Services;
    using Features.Listing.Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Features.Deal.Services;
    using Features.Deal.Services.Interfaces;
    using Seller.Listings.Features.Seller.Services;
    using Seller.Listings.Features.Seller.Services.Interfaces;
    using Filters;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services) => services
            .AddTransient<ISellerService, SellerService>()
            .AddTransient<IListingService, ListingService>()
            .AddTransient<IDealService, DealService>();
          

        public static IServiceCollection AddSwagger(this IServiceCollection services) => services
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() {Title = "Seller API", Version = "v1"});
            });

        public static void AddApiControllers(this IServiceCollection services) =>
            services.AddControllers(option => option.Filters.Add<ModelOrNotFoundActionFilter>());

    }
}
