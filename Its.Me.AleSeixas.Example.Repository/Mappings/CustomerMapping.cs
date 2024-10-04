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
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasIndex(p => p.Code).IsUnique();
            builder.HasIndex(p => p.Token).IsUnique();
            builder.Property(e => e.Code).HasMaxLength(8);
            builder.Property(e => e.Token).HasMaxLength(6);

            builder.HasOne(c => c.Personal)
                   .WithOne(p => p.Customer)
                   .HasForeignKey<Customer>(p => p.IdPersonal);
        }
    }
}
