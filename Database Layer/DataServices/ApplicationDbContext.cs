using Database_Layer.DatabaseEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Layer.DataServices
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "your_connection_string",
                    b => b.MigrationsAssembly("Database Layer")); // Specify the migrations assembly here
            }
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.TransactionId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<TransactionDetail>()
                .HasKey(td => td.TransactionDetailId);

            modelBuilder.Entity<TransactionDetail>()
                .HasOne(td => td.Transaction)
                .WithMany(t => t.TransactionDetails)
                .HasForeignKey(td => td.TransactionId);

            modelBuilder.Entity<TransactionDetail>()
                .HasOne(td => td.Product)
                .WithMany(p => p.TransactionDetails)
                .HasForeignKey(td => td.ProductId);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);
        }
    }
}
