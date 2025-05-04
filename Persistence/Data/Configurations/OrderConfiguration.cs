﻿using Domain.Entities.OrderEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, address => address.WithOwner());

            builder.HasMany(o => o.OrderItems).WithOne(); 

            builder.Property(o => o.PaymentStatus)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderPaymentStatus)Enum.Parse(typeof(OrderPaymentStatus), o))
                .IsRequired();
        }
    }
}
