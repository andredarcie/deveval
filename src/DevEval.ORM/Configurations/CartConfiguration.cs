using DevEval.Domain.Entities.Cart;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevEval.ORM.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("cart");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id")
                .UseIdentityAlwaysColumn();

            builder.Property(c => c.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            builder.Property(c => c.Date)
                .IsRequired()
                .HasColumnName("date")
                .HasColumnType("TIMESTAMPTZ");

            builder.HasMany(c => c.Products)
                .WithOne()
                .HasForeignKey("cart_id")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}