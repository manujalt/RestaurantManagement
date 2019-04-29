using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystems.Models;
using RestaurantManagementSystems.PayrollSystem.Models;
using coderush.Models;


namespace RestaurantManagementSystems.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderItems { get; set; }

        public DbSet<MenuItem> MenuItmes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail");
            modelBuilder.Entity<MenuItem>().ToTable("MenuItem");

            
           
        }

        public DbSet<RestaurantManagementSystems.PayrollSystem.Models.Employee> Employee { get; set; }

        public DbSet<RestaurantManagementSystems.PayrollSystem.Models.Salaries> Salaries { get; set; }

        public DbSet<RestaurantManagementSystems.PayrollSystem.Models.Department> Department { get; set; }

        public DbSet<RestaurantManagementSystems.PayrollSystem.Models.Dependent> Dependent { get; set; }

        public DbSet<coderush.Models.Vendor> Vendor { get; set; }

        public DbSet<coderush.Models.VendorType> VendorType { get; set; }

        public DbSet<coderush.Models.Product> Product { get; set; }

        public DbSet<coderush.Models.PurchaseType> PurchaseType { get; set; }

        public DbSet<coderush.Models.PurchaseOrder> PurchaseOrder { get; set; }

        public DbSet<coderush.Models.PurchaseOrderLine> PurchaseOrderLine { get; set; }
    }
}
