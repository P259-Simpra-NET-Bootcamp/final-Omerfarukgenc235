using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpraBitirme.EntityLayer.Concrete;

namespace SimpraBitirme.DataAccessLayer.Mapper
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.CreatedAt).IsRequired(true);
            builder.Property(x => x.CreatedBy).IsRequired(true);

            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(30);

            builder.HasIndex(x => x.Name).IsUnique(true);

            
        }
    }
}