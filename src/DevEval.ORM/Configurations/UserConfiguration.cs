using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DevEval.Domain.Entities.User;

namespace DevEval.ORM.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .UseIdentityAlwaysColumn();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(255);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasColumnName("username")
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasMaxLength(255);

            builder.OwnsOne(u => u.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(100)
                    .IsRequired();

                nameBuilder.Property(n => n.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(100)
                    .IsRequired();
            });

            builder.OwnsOne(u => u.Address, addressBuilder =>
            {
                addressBuilder.Property(a => a.Street)
                    .HasColumnName("street")
                    .HasMaxLength(255)
                    .IsRequired();

                addressBuilder.Property(a => a.City)
                    .HasColumnName("city")
                    .HasMaxLength(100)
                    .IsRequired();

                addressBuilder.Property(a => a.Number)
                    .HasColumnName("number")
                    .IsRequired();

                addressBuilder.Property(a => a.ZipCode)
                    .HasColumnName("zip_code")
                    .HasMaxLength(20)
                    .IsRequired();

                addressBuilder.OwnsOne(a => a.Geolocation, geoBuilder =>
                {
                    geoBuilder.Property(g => g.Lat)
                        .HasColumnName("latitude")
                        .IsRequired();

                    geoBuilder.Property(g => g.Long)
                        .HasColumnName("longitude")
                        .IsRequired();
                });
            });

            builder.Property(u => u.Phone)
                .HasColumnName("phone")
                .HasMaxLength(20);

            builder.Property(u => u.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasConversion<int>();

            builder.Property(u => u.Role)
                .IsRequired()
                .HasColumnName("role")
                .HasConversion<int>();
        }
    }
}
