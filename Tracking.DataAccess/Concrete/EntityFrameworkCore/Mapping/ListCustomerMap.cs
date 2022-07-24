using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Entities.Concrete;

namespace Tracking.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class ListCustomerMap : IEntityTypeConfiguration<ListCustomer>
    {
        public void Configure(EntityTypeBuilder<ListCustomer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.HasIndex(x => new { x.ListId, x.CustomerId });
        }
    }
}
