namespace Seller.Identity.Infrastructure.Extensions
{
    using Data;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
 
    public static class ApplicationBuilderExtensions
    {

        public static void ApplyRoles(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var context = services.ServiceProvider.GetRequiredService<IdentityDbContext>();

            if (context.Roles.Count() <= 1)
            {
                context.Roles.Add(new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" });
                context.Roles.Add(new IdentityRole() { Name = "User", NormalizedName = "USER" });
                context.Roles.Add(new IdentityRole() { Name = "Guest", NormalizedName = "GUEST" });
                context.SaveChanges();
            }


        }
    }
}
