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
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.Property(P => P.Id)
               .IsRequired();


			builder.Property(P => P.Name)
                .IsRequired()
                .HasMaxLength(100);



            builder.Property(P => P.Price)
              .HasColumnType("decimal(18,2)");




        }
    }
}
