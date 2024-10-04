using Its.Me.AleSeixas.Example.Domina.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Repository.Mappings
{
    public class PersonalMapping : IEntityTypeConfiguration<Personal>
    {
        public void Configure(EntityTypeBuilder<Personal> builder)
        {
            builder.HasIndex(p => p.Document).IsUnique();
            builder.Property(e => e.Document).HasMaxLength(100);
            builder.Property(e => e.Name).HasMaxLength(100);
        }
    }
}
