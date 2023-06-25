using Microsoft.EntityFrameworkCore;
using SimpraBitirme.DataAccessLayer.Mapper;
using SimpraBitirme.EntityLayer.Concrete;

namespace SimpraBitirme.DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<BankCard> BankCards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BasketMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new BasketItemMap());
            modelBuilder.ApplyConfiguration(new OrderItemMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new CouponMap());
            modelBuilder.ApplyConfiguration(new BankCardMap());


            //modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.Entity<CategoryProduct>()
                 .HasOne(cp => cp.Category)
                 .WithMany(c => c.CategoryProducts)
                 .HasForeignKey(cp => cp.CategoryId)
                 .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<CategoryProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.CategoryProducts)
                .HasForeignKey(cp => cp.ProductId)
                .OnDelete(DeleteBehavior.Cascade); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
