using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    internal class DeliveryMethodConf : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("Delivery Method");
            builder.Property(d => d.Price).HasColumnType("decimal(8,2)");

            builder.Property(d => d.ShortName).HasColumnType("varchar(50)");

            builder.Property(d => d.Description).HasColumnType("varchar(100)");

            builder.Property(d => d.DeliveryTime).HasColumnType("varchar(50)");
        }
    }
}
