using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OrdersManagement.Core.Entities;
using OrdersManagement.Repository.Identity;

namespace OrdersManagement.Repository.Data
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
			modelBuilder.Seed();



		}



		public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems{ get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }

	}

}
