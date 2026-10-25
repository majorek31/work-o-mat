using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkOMat.Domain.User;

namespace WorkOMat.Infrastructure.Persistance.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id)
                .HasConversion(
                    id => id.Value,
                    value => new UserId(value)
                );
        builder.Property(u => u.IdentityId)
            .HasConversion(
                id => id.Value,
                value => new IdentityUserId(value));

        builder.HasKey(u => u.Id);
        builder.Property(c => c.DisplayName)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.HasIndex(u => u.IdentityId)
            .IsUnique();
    }
}