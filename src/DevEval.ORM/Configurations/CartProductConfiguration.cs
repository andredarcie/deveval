using DevEval.Domain.Entities.Cart;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevEval.ORM.Configurations
{
    public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.ToTable("cart_product");

            builder.HasKey(cp => cp.Id);

            builder.Property(cp => cp.Id)
                .HasColumnName("id")
                .UseIdentityAlwaysColumn();

            builder.Property(cp => cp.ProductId)
                .IsRequired()
                .HasColumnName("product_id");

            builder.Property(cp => cp.UnitPrice)
                .IsRequired()
                .HasColumnName("unit_price")
                .HasColumnType("NUMERIC(10,2)");

            builder.Property(cp => cp.Quantity)
                .IsRequired()
                .HasColumnName("quantity");
        }
    }
}
