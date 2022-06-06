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
    public class BasketProductMap : IEntityTypeConfiguration<BasketProduct>
    {
        public void Configure(EntityTypeBuilder<BasketProduct> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.HasIndex(x => new { x.ProductId, x.BasketId });
        }
    }
}
