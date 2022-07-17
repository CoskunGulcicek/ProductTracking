using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class ListMap : IEntityTypeConfiguration<Entities.Concrete.List>
    {
        public void Configure(EntityTypeBuilder<Entities.Concrete.List> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
