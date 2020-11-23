namespace Seller.Listing.Gateway.Infrastructure.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Filters;
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddSwagger(this IServiceCollection services) => services
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() {Title = "Seller API", Version = "v1"});
            });

        public static void AddApiControllers(this IServiceCollection services) =>
            services.AddControllers(option => option.Filters.Add<ModelOrNotFoundActionFilter>());

    }
}
