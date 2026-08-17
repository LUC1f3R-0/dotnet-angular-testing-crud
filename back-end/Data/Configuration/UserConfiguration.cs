using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
        .ValueGeneratedOnAdd();

        builder.Property(u => u.FirstName)
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(u => u.LastName)
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(u => u.Email)
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(u => u.Age)
        .IsRequired();

        builder.HasIndex(u => u.Email)
        .IsUnique();
    }
}