﻿using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configuration
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasOne(p => p.ProductBrand)
                .WithMany()
                .HasForeignKey(p => p.ProductBrandId)
                ;
            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.ProductTypeId);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Description)
                .IsRequired();
            builder.Property(p => p.PictureUrl)
                .IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(10,2)");
        }
    }
}
