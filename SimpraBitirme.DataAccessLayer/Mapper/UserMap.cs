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
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("User");

            builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.CreatedAt).IsRequired(true);
            builder.Property(x => x.CreatedBy).IsRequired(true);

            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(70);
            builder.Property(x => x.Surname).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Role).IsRequired(true).HasMaxLength(1);
            builder.Property(x => x.Password).IsRequired(true).HasMaxLength(300);

            builder.HasIndex(x => x.Email).IsUnique(true);

            builder.HasMany(x => x.Order)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId)
               .IsRequired(true);

            builder.HasMany(x => x.Basket)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId)
               .IsRequired(true);

            builder.HasMany(x => x.Coupon)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId)
               .IsRequired(true);

        }
    }
}
