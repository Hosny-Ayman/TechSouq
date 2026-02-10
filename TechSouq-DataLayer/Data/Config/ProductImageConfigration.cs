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
    public class ProductImageConfigration:IEntityTypeConfiguration<ProductImage>
    {

        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ImageUrl).HasColumnType("NVARCHAR(500)").IsRequired();

            builder.HasOne(x => x.Product).WithMany(x => x.ProductImages).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);


        }

    }
}
