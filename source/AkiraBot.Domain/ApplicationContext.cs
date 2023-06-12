using AkiraBot.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AkiraBot.Domain;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<LogType> LogTypes { get; set; }
    public DbSet<OrderType> OrderTypes { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<SpotOrder> SpotOrders  { get; set; }
    public DbSet<TakeProfitStopLossOrder> TakeProfitStopLossOrders { get; set; }
    public DbSet<ArbitrageOrder> ArbitrageOrders { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies()
            .UseSqlServer(@"Data Source=DESKTOP-HOARC76;Initial Catalog=AkiraBotDb;Integrated Security=True;MultipleActiveResultSets=true; 
            Trusted_Connection=True;TrustServerCertificate=True;");
    }
}