using DevEval.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DevEval.ORM.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .UseIdentityAlwaysColumn();

            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnName("title")
                .HasMaxLength(255);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnName("price")
                .HasColumnType("NUMERIC(10,2)");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnName("description")
                .HasColumnType("TEXT");

            builder.Property(p => p.Category)
                .HasColumnName("category")
                .HasMaxLength(100);

            builder.Property(p => p.Image)
                .IsRequired()
                .HasColumnName("image")
                .HasMaxLength(500);

            builder.OwnsOne(p => p.Rating);
        }
    }
}
