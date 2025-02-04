using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DevEval.Domain.Entities.Sale;

namespace DevEval.ORM.Configurations
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("sale_item");

            builder.HasKey(si => si.Id);

            builder.Property(si => si.Id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(si => si.ProductId)
                .IsRequired()
                .HasColumnName("product_id");

            builder.Property(si => si.Quantity)
                .IsRequired()
                .HasColumnName("quantity");

            builder.Property(si => si.UnitPrice)
                .IsRequired()
                .HasColumnName("unit_price")
                .HasColumnType("NUMERIC(10,2)");

            builder.Property(si => si.Discount)
                .IsRequired()
                .HasColumnName("discount")
                .HasColumnType("NUMERIC(5,2)");

            builder.Property(si => si.IsCancelled)
                .IsRequired()
                .HasColumnName("is_cancelled");

            builder.Ignore(si => si.TotalPrice);
        }
    }
}