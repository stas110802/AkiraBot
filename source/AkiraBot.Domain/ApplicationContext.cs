using AkiraBot.Domain.Models;
using AkiraBot.Utilities.CommonTools.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using User = AkiraBot.Domain.Models.User;

namespace AkiraBot.Domain;

public class ApplicationContext : DbContext
{
    private static string ConnectionString = @"Data Source=DESKTOP-HOARC76;Initial Catalog=AkiraBotDb;Integrated Security=True;MultipleActiveResultSets=true; 
            Trusted_Connection=True;TrustServerCertificate=True;";
    
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
            .UseSqlServer(ConnectionString);
    }

    public static void RunSqlCreateDbFile()
    {
        var pathList = new PathList();
        var filePath = $@"{pathList.SqlQueryPath}SQLQuery1.sql";
        var script = File.ReadAllText(filePath);
        var conn = new SqlConnection(ConnectionString);
        var server = new Server(new ServerConnection(conn));
        try
        {
            server.ConnectionContext.ExecuteNonQuery(script);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}