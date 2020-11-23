namespace Seller.Messages.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    public class MessageDbContext: DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public MessageDbContext(DbContextOptions<MessageDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
