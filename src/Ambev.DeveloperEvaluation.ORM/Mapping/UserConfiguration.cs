using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

/// <summary>
/// Configures the mapping of the <see cref="User"/> entity to the database schema.
/// This class defines table name, primary key, property configurations, and relationships.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configures the database schema for the <see cref="User"/> entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        // Configures the primary key and its default value generation.
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        // Configures the Username property with required constraints and max length.
        builder.Property(u => u.Username).IsRequired().HasMaxLength(50);

        // Configures the Password property with required constraints and max length.
        builder.Property(u => u.Password).IsRequired().HasMaxLength(100);

        // Configures the Email property with required constraints and max length.
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);

        // Configures the Phone property with optional constraints and max length.
        builder.Property(u => u.Phone).HasMaxLength(20);

        // Configures the Status property with string conversion and max length.
        builder.Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        // Configures the CreatedAt property with a default value of the current timestamp.
        builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired();

        // Configures the UpdatedAt property as optional.
        builder.Property(u => u.UpdatedAt);

        // Configures the Name value object as an owned entity.
        builder.OwnsOne(
           u => u.Name,
           name =>
           {
               name.Property(n => n.FirstName)
                   .HasColumnName("FirstName")
                   .IsRequired()
                   .HasMaxLength(50);

               name.Property(n => n.LastName)
                   .HasColumnName("LastName")
                   .IsRequired()
                   .HasMaxLength(50);
           });

        // Configures the Address value object as an owned entity.
        builder.OwnsOne(
            u => u.Address,
            address =>
            {
                address.Property(a => a.City)
                    .HasColumnName("City")
                    .IsRequired()
                    .HasMaxLength(100);

                address.Property(a => a.Street)
                    .HasColumnName("Street")
                    .IsRequired()
                    .HasMaxLength(100);

                address.Property(a => a.Number)
                    .HasColumnName("Number")
                    .HasMaxLength(10)
                    .IsRequired();

                address.Property(a => a.ZipCode)
                    .HasColumnName("ZipCode")
                    .IsRequired()
                    .HasMaxLength(20);

                address.Property(a => a.State)
                    .HasColumnName("State")
                    .IsRequired()
                    .HasMaxLength(2);

                // Configures the GeoLocation value object as an owned entity within Address.
                address.OwnsOne(
                    a => a.GeoLocation,
                    geo =>
                    {
                        geo.Property(g => g.Latitude)
                            .HasColumnName("Latitude")
                            .IsRequired()
                            .HasMaxLength(50);

                        geo.Property(g => g.Longitude)
                            .HasColumnName("Longitude")
                            .IsRequired()
                            .HasMaxLength(50);
                    });
            });

        // Configures the Role property with string conversion and max length.
        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}
