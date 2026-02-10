using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq.Domain.Entities;

namespace TechSouq.Infrastructure.Data.Config
{
    public class OrderItemConfigration:IEntityTypeConfiguration<OrderItem>
    {

        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity).HasColumnType("Integer").IsRequired();

            builder.Property(x => x.UnitPrice).HasColumnType("Decimal(10,2)").IsRequired();

            builder.HasOne(x => x.Order).WithMany(x=>x.OrderItems).HasForeignKey(x=>x.OrderId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Product).WithMany(x => x.OrderItems).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);



        }

    }
}
