using Microsoft.EntityFrameworkCore;

namespace Entities;

public class StockMarketDbContext : DbContext 
{
    public DbSet<BuyOrder> BuyOrders { get; set; }
    public DbSet<SellOrder> SellOrders { get; set; }

    public StockMarketDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BuyOrder>().ToTable("BuyOrders");
        modelBuilder.Entity<SellOrder>().ToTable("SellOrders");
    }
}