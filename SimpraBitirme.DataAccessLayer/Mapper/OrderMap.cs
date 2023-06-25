using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpraBitirme.EntityLayer.Concrete;

namespace SimpraBitirme.DataAccessLayer.Mapper
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
            builder.Property(x => x.CreatedAt).IsRequired(true);
            builder.Property(x => x.CreatedBy).IsRequired(true);

            builder.HasIndex(x => x.OrderNumber).IsUnique(true);

            builder.HasMany(x => x.OrderItem)
                 .WithOne(x => x.Order)
                 .HasForeignKey(x => x.OrderId)
                 .IsRequired(true);
        }
    }    
}
