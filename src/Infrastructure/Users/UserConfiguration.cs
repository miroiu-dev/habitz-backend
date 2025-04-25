using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.HasMany(u => u.Habits).WithOne(h => h.User).HasForeignKey(h => h.UserId);
        builder.HasMany(u => u.BodyMeasurementLogs).WithOne(h => h.User).HasForeignKey(h => h.UserId);
    }
}
