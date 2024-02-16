using Limak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Limak.Persistence.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(x => x.AppUserId).IsRequired();
        builder.Property(x => x.ChatId).IsRequired();
        builder.Property(x => x.Body).IsRequired().HasMaxLength(4096);

    }
}
