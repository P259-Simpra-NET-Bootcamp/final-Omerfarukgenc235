using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimpraBitirme.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpraBitirme.DataAccessLayer.Mapper
{
    public class BasketMap : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.ToTable("Basket");

            builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.CreatedAt).IsRequired(true);
            builder.Property(x => x.CreatedBy).IsRequired(true);


            builder.HasMany(x => x.BasketItems)
                 .WithOne(x => x.Basket)
                 .HasForeignKey(x => x.BasketId)
                 .IsRequired(true);
        }
    }
}
