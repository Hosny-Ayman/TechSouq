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
    internal class CategorieConfigration:IEntityTypeConfiguration<Categorie>
    {

        public void Configure(EntityTypeBuilder<Categorie> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasColumnType("NVARCHAR(150)").IsRequired();
        }

    }
}
