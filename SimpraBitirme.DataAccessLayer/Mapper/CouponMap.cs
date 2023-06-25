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
    public class CouponMap : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("Coupon");

            builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.Code).IsRequired(true);
            builder.Property(x => x.ExpireDate).IsRequired(true);
            builder.Property(x => x.CreatedAt).IsRequired(true);
            builder.Property(x => x.CreatedBy).IsRequired(true);
            builder.Property(x => x.Status).IsRequired(true);

            builder.HasIndex(x => x.Code).IsUnique(true);

        }
    }
}
