using Limak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Limak.Persistence.Configurations;

public class DeliverConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.Property(x => x.Region).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Village).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Street).IsRequired().HasMaxLength(128);
        builder.Property(x => x.HomeNo).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Phone).IsRequired();
        builder.Property(x => x.Notes).HasMaxLength(1024);

    }
}
