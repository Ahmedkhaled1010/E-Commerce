using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            
            builder.Property(o => o.SubTotal).HasColumnType("decimal(8,2)");
            builder.HasMany(o => o.Items).WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.DeliveryMethods).WithMany()
                .HasForeignKey(o=>o.DeliveryMethodsId);
            builder.OwnsOne(o => o.shipToAddress);
        }
    }
}
