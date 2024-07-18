using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrdersManagement.Core.Entities;
using OrdersManagement.Repository.Identity;

namespace OrdersManagement.Repository.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<User>
    {

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedRoles();
            modelBuilder.SeedAdminUser();


        }


	}
}
