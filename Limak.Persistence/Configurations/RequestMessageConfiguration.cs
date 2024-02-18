using Limak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Limak.Persistence.Configurations;

public class RequestMessageConfiguration : IEntityTypeConfiguration<RequestMessage>
{
    public void Configure(EntityTypeBuilder<RequestMessage> builder)
    {
        builder.Property(x => x.AppUserId).IsRequired();
        builder.Property(x => x.RequestId).IsRequired();
        builder.Property(x => x.Message).IsRequired().HasMaxLength(4096);

    }
}
