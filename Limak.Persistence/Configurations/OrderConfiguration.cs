using Limak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Limak.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.OrderURL).IsRequired().HasMaxLength(1028);
        builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.LocalCargoPrice).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(x => x.CargoPrice).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(x => x.Weight).HasColumnType("decimal(10,3)");
        builder.Property(x => x.OrderPaymentStatus).HasDefaultValue(false);
        builder.Property(x => x.CargoPaymentStatus).HasDefaultValue(false);
        builder.Property(x => x.Count).HasDefaultValue(1);
        builder.Property(x => x.Color).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Size).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Notes).IsRequired().HasMaxLength(256);
        builder.Property(x => x.AdditionFees).HasColumnType("decimal(18,2)");
        builder.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");
        builder.Property(x => x.OrderTotalPrice).HasColumnType("decimal(18,2)");
        builder.Property(x => x.AdditionFeesNotes).HasMaxLength(256);
        builder.Property(x => x.CancellationNotes).HasMaxLength(256);

        builder.Property(x => x.AppUserId).IsRequired();
        builder.Property(x => x.WarehouseId).IsRequired();
        builder.Property(x => x.CountryId).IsRequired();

    }
}
