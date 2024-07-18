using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersManagement.Core.Entities;

namespace OrdersManagement.Repository.Data.Cofigurations
{
    public class OrderConfigurations  : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.Property(O => O.Id)
               .IsRequired();


            builder.Property(O => O.TotalAmount)
               .HasColumnType("decimal(18,2)");


			builder.HasMany(O => O.OrderItems)
				.WithOne(oi => oi.Order)
				   .HasForeignKey(OI => OI.OrderId)
				   .OnDelete(DeleteBehavior.Cascade);



			builder.HasOne(O => O.Customer)
				   .WithMany(C => C.Orders)
				   .HasForeignKey(O => O.CustomerId)
				   .OnDelete(DeleteBehavior.Cascade);

			

		}
	}
}
