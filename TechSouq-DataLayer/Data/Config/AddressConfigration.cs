using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechSouq_DataLayer.Models;

namespace TechSouq_DataLayer.Data.Config
{
    public class AddressConfigration : IEntityTypeConfiguration<Address>
    {

        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Street).HasColumnType("NVARCHAR(250)").IsRequired();

            builder.Property(x => x.City).HasColumnType("NVARCHAR(100)").IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.Addresses).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

        }

    }
}
