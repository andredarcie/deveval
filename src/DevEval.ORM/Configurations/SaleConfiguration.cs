using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DevEval.Domain.Entities.Sale;

namespace DevEval.ORM.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("sale");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(s => s.SaleNumber)
                .IsRequired()
                .HasColumnName("sale_number")
                .HasMaxLength(50);

            builder.Property(s => s.SaleDate)
                .IsRequired()
                .HasColumnName("sale_date")
                .HasColumnType("TIMESTAMPTZ");

            builder.Property(s => s.IsCancelled)
                .IsRequired()
                .HasColumnName("is_cancelled");

            builder.Property(s => s.CustomerId)
                .IsRequired()
                .HasColumnName("customer_id");

            builder.Property(s => s.CustomerName)
                .IsRequired()
                .HasColumnName("customer_name")
                .HasMaxLength(255);

            builder.Property(s => s.BranchId)
                .IsRequired()
                .HasColumnName("branch_id");

            builder.Property(s => s.BranchName)
                .IsRequired()
                .HasColumnName("branch_name")
                .HasMaxLength(255);

            builder.Ignore(s => s.TotalAmount);

            builder.Navigation(s => s.Items)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}