using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagement.Repository.Data.Cofigurations
{
	public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
	{
		public void Configure(EntityTypeBuilder<Customer> builder)
		{
			builder.Property(C => C.Id)
				   .IsRequired();

			builder.Property(c => c.Name)
				   .IsRequired()
				   .HasMaxLength(100);

			builder.Property(C => C.Email)
					.IsRequired();


			builder.HasMany(C => C.Orders)
				   .WithOne(o => o.Customer)
			       .HasForeignKey(O => O.CustomerId)
				   .OnDelete(DeleteBehavior.Cascade);



		
		}
	}
}