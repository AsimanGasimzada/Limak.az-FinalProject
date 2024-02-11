using Limak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Limak.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.Property(x => x.Subject).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(1024);
        builder.Property(x => x.AppUserId).IsRequired();
        builder.Property(x => x.IsRead).HasDefaultValue(false);
    }
}
