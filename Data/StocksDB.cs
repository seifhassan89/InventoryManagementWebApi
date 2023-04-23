using Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class StocksDB : DbContext
    {
        public StocksDB(DbContextOptions<StocksDB> options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<MeasuringUnit> MeasuringUnits { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestItem> RequestItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockItem> StockItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
             .Entity<Stock>()
             .HasMany(p => p.SupplyRequests)
             .WithOne(p => p.StockTo);
            modelBuilder
             .Entity<Stock>()
             .HasMany(p => p.WithdrawRequests)
             .WithOne(p => p.StockFrom);

            modelBuilder.ApplyConfiguration(new RoleConfigurations());
            modelBuilder.ApplyConfiguration(new RequestTypeConfigurations());

        }

    }
}