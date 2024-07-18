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
    public class InvoiceConfigurations : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {

            builder.Property(I => I.Id)
               .IsRequired();


			builder.Property(I => I.TotalAmount )
				   .HasColumnType("decimal(18,2)");


			builder.HasOne(I => I.Order )
			  .WithMany()
			  .HasForeignKey(I => I.OrderId);


			

		}
	}
}
