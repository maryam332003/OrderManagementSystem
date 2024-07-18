using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrdersManagement.Core.Entities;
using OrdersManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Repository.Data
{
    public static class OrderSeeding
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Coffee", Price = 2.99M, Stock = 100 },
                new Product { Id = 2, Name = "Tea", Price = 1.99M, Stock = 200 },
                new Product { Id = 3, Name = "Cake", Price = 4.99M, Stock = 50 },
                new Product { Id = 4, Name = "Cookie", Price = 0.99M, Stock = 300 }
            );

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "John Doe", Email = "john.doe@example.com",Orders=new List<Order> ()},
                new Customer { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" ,Orders=new List<Order>()}
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, CustomerId = 1, OrderDate = DateTime.Now, TotalAmount = 10.97M, PaymentMethod = PaymentMethodsEnum.CreditCard, Status = OrderStatusEnum.Pending },
                new Order { Id = 2, CustomerId = 2, OrderDate = DateTime.Now, TotalAmount = 5.98M, PaymentMethod = PaymentMethodsEnum.MasterCard, Status = OrderStatusEnum.PaymentRecieved }
            );

            // Seed OrderItems
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 2.99M, Discount = 0 },
                new OrderItem { Id = 2, OrderId = 1, ProductId = 4, Quantity = 2, UnitPrice = 0.99M, Discount = 0 },
                new OrderItem { Id = 3, OrderId = 2, ProductId = 2, Quantity = 3, UnitPrice = 1.99M, Discount = 0 }
            );


        }
    }
}

