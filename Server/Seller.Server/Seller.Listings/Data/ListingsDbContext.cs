namespace Seller.Listings.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Reflection;
    using Seller.Shared.Data;

    public class ListingsDbContext : MessageDbContext
    {
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<UserSeller> UserSellers { get; set; }

        public ListingsDbContext(DbContextOptions<ListingsDbContext> options)
            : base(options)
        {
        }

        protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Listing>()
                .HasOne<Deal>(a => a.Deal)
                .WithOne(b => b.Listing)
                .HasForeignKey<Deal>(a => a.ListingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Deal>()
                .HasOne(u => u.Seller)
                .WithMany(s => s.SaleDeals)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Deal>()
                .HasOne(u => u.Buyer)
                .WithMany(s => s.BuyDeals)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
