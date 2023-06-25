using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpraBitirme.EntityLayer.Concrete;

namespace SimpraBitirme.DataAccessLayer.Mapper
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.CreatedAt).IsRequired(true);
            builder.Property(x => x.CreatedBy).IsRequired(true);

            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.Url).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.Tag).IsRequired(true).HasMaxLength(100);

            builder.HasIndex(x => x.Name).IsUnique(true);

          

            builder.HasMany(x => x.BasketItems)
             .WithOne(x => x.Product)
             .HasForeignKey(x => x.ProductId)
             .IsRequired(true);
        }
    }
}
