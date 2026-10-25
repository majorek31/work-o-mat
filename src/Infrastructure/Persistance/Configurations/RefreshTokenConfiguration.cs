using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkOMat.Domain.RefreshToken;
using WorkOMat.Domain.User;

namespace WorkOMat.Infrastructure.Persistance.Configurations;

public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.Property(rt => rt.Id).HasConversion(
            id => id.Value,
            value => new RefreshTokenId(value)
        );
        builder.HasKey(rt => rt.Id);
        builder.Property(rt => rt.Token).IsRequired();
        builder.Property(rt => rt.ExpiresAt).IsRequired();
        builder.Property(rt => rt.UserId).IsRequired();
        builder.HasOne<User>().WithMany().HasForeignKey(rt => rt.UserId);
    }
}