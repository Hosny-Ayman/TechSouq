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
    public class DeliveryMethodConfigration:IEntityTypeConfiguration<DeliveryMethod>
    {

        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnType("NVARCHAR(50)").IsRequired();

            builder.Property(x => x.Cost).HasColumnType("DECIMAL(10,2)").IsRequired();
        }

    }
}
