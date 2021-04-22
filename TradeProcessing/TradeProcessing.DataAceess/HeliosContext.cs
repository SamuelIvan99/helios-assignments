using Microsoft.EntityFrameworkCore;
using TradeProcessing.Model;

namespace TradeProcessing.DataAceess
{
    public class HeliosContext : DbContext
    {
        public HeliosContext(DbContextOptions<HeliosContext> options) : base(options) { }

        public DbSet<Trade> Trades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trade>().ToTable("Trade");
        }
    }
}
