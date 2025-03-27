
using Microsoft.EntityFrameworkCore;
using MonitorExchange.Models;

namespace MonitorExchange.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
           

        }

        public DbSet<FileExchange> FileExchanges => Set<FileExchange>();
        public DbSet<Goods> Goodses => Set<Goods>();
        public DbSet<GoodsSize> GoodsSizes => Set<GoodsSize>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<User> Users => Set<User>();
        public DbSet<FEImport> FEImports => Set<FEImport>();
        public DbSet<FEOffers> FEOffers => Set<FEOffers>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FEImport>()
                .HasOne(f => f.Goods)
                .WithMany(g => g.FEImports)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FEImport>()
                .HasOne(f => f.FileExchange)
                .WithMany(g => g.FEImports)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FEImport>()
                .HasOne(f => f.GoodsSize)
                .WithMany(g => g.FEImports)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FEOffers>()
                .HasOne(f => f.Goods)
                .WithMany(g => g.FEOffers)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FEOffers>()
                .HasOne(f => f.FileExchange)
                .WithMany(g => g.FEOffers)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FEOffers>()
                .HasOne(f => f.GoodsSize)
                .WithMany(g => g.FEOffers)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FEOffers>()
                .HasOne(f => f.Stock)
                .WithMany(g => g.FEOffers)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GoodsSize>()
                .HasOne(f => f.Goods)
                .WithMany(g => g.GoodsSizes)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
