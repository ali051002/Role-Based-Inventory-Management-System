using IMS.Domain.Entities;
using IMS.Shared.DTOs.Product.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.DbContext
{
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
    : base(options)
        {
        }

        // Auth
        //public DbSet<User> Users => Set<User>();
        //public DbSet<Role> Roles => Set<Role>();
        //public DbSet<UserRole> UserRoles => Set<UserRole>();


        #region Models
        // Inventory
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<StockTransaction> StockTransactions { get; set; } = null!;
        #endregion

        #region VMs

        [NotMapped]
        public DbSet<ProductDetailResponseDto> ProductDetailResponseDto { get; set; } = null!;

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// UserRoles (Composite Key)
            //modelBuilder.Entity<UserRole>()
            //    .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductCode)
                .IsUnique();

            modelBuilder.Entity<StockTransaction>()
                .Property(s => s.TransactionType)
                .HasMaxLength(10);

            //// Seed Roles
            //modelBuilder.Entity<Role>().HasData(
            //    new Role { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Admin" },
            //    new Role { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "User" }
            //);
        }
    }
}
