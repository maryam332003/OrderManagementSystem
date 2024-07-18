using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrdersManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Repository.Identity
{
    public static class UserSeeding
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" }
            );
        }

        public static void SeedAdminUser(this ModelBuilder modelBuilder)
        {
            var adminUser = new User
            {
                Id = "1",
                UserName = "maryamgm3323@gmail.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                Email = "maryamgm3323@gmail.com",
                NormalizedEmail = "MARYAMGM3323@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var passwordHasher = new PasswordHasher<User>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Maram@332003");

            modelBuilder.Entity<User>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUser.Id, RoleId = "1" }
            );
        }
    }
}

