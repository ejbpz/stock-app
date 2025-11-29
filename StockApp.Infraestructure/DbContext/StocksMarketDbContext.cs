using Microsoft.EntityFrameworkCore;

namespace StockApp.Models
{
    public class StocksMarketDbContext : DbContext
    {
        public DbSet<BuyOrder> BuyOrders { get; set; }
        public DbSet<SellOrder> SellOrders { get; set; }

        public StocksMarketDbContext(DbContextOptions<StocksMarketDbContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
