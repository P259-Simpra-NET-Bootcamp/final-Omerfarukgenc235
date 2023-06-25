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
    public class BankCardMap : IEntityTypeConfiguration<BankCard>
    {
        public void Configure(EntityTypeBuilder<BankCard> builder)
        {
            builder.ToTable("BankCard");

            builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.CreatedAt).IsRequired(true);
            builder.Property(x => x.CreatedBy).IsRequired(true);
            builder.Property(x => x.CVV).IsRequired(true).HasMaxLength(3);
            builder.Property(x => x.CardNumber).IsRequired(true);
            builder.Property(x => x.Expiration).IsRequired(true);
            builder.Property(x => x.CardName).IsRequired(true).HasMaxLength(200);

            builder.HasIndex(x => x.CardNumber).IsUnique(true);
        }
    }
}
