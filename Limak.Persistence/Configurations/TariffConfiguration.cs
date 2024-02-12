using Limak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Limak.Persistence.Configurations;

public class TariffConfiguration : IEntityTypeConfiguration<Tariff>
{
    public void Configure(EntityTypeBuilder<Tariff> builder)
    {
        builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.MaxValue).IsRequired().HasColumnType("decimal(5,3)");
        builder.Property(x => x.MinValue).IsRequired().HasColumnType("decimal(5,3)");
        builder.Property(x => x.CountryId).IsRequired();

    }
}
