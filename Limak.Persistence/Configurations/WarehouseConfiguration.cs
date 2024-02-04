using Limak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Limak.Persistence.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Location).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Position).IsRequired().HasMaxLength(128);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(256);
        builder.Property(x => x.WorkingHours).IsRequired().HasMaxLength(256);
    }
}
