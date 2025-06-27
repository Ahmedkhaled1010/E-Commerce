using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethods>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethods> builder)
        {
            builder.ToTable("DeliveryMethod");
            builder.Property(d => d.Cost).
                HasColumnType("decimal(8,2)");
            builder.Property(d=>d.ShortName).
                HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(d => d.Description).
               HasColumnType("varchar")
               .HasMaxLength(100);
            builder.Property(d => d.DeliveryTime).
               HasColumnType("varchar")
               .HasMaxLength(50);
        }
    }
}
