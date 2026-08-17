using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.id);

        builder.Property(u => u.id)
        .ValueGeneratedOnAdd();

        builder.Property(u => u.uuid)
        .HasDefaultValueSql("gen_random_uuid()")
        .ValueGeneratedOnAdd();

        builder.Property(u => u.firstName)
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(u => u.lastName)
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(u => u.email)
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(u => u.age)
        .IsRequired();

        builder.HasIndex(u => u.email)
        .IsUnique();
    }
}