using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersManagement.Core.Entities;
using System.Reflection.Emit;

namespace OrdersManagement.Repository.Data.Cofigurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {

            builder.Property(OI => OI.Id)
               .IsRequired();


            builder.Property(OI => OI .UnitPrice)
                   .HasColumnType("decimal(18,2)");

			builder.Property(OI => OI.Discount)
                   .HasColumnType("decimal(18,2)");





            builder.HasOne(OI => OI.Product)
                   .WithMany()
                   .HasForeignKey(OI => OI.ProductId);



			




		}
	}
}
