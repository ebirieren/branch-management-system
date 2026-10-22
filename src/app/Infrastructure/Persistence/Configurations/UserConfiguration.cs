namespace Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.User;

public sealed class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("Users");

        entity.HasKey(user => user.Id);

        entity.Property(user => user.Id)
            .IsRequired()
            .HasColumnName("id");

        entity.HasIndex(user => user.Id)
            .IsUnique();

        entity.Property(user => user.Name)
            .HasColumnName("userName");

        entity.Property(user => user.Email)
            .HasColumnName("email");
        
        entity.Property(user => user.PasswordHash)
            .HasColumnName("passwordHash");

        entity.Property(user => user.Role)
            .HasColumnName("role");
    }
}