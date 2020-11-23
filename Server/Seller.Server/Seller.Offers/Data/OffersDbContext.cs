namespace Seller.Offers.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    public class OffersDbContext : DbContext
    {
        public DbSet<Offer> Offers { get; set; }

        public OffersDbContext(DbContextOptions<OffersDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
