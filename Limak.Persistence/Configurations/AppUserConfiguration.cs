using Limak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Limak.Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(x => x.UserName).IsRequired(false);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Surname).IsRequired().HasMaxLength(128);
        builder.Property(x => x.SeriaNumber).IsRequired().HasMaxLength(16);
        builder.Property(x => x.FinCode).IsRequired().HasMaxLength(7).IsFixedLength();
        builder.HasIndex(x => x.SeriaNumber).IsUnique();
        builder.HasIndex(x => x.FinCode).IsUnique();
        builder.Property(x => x.Birtday).IsRequired();
        builder.Property(x => x.Location).IsRequired().HasMaxLength(256);
        builder.Property(x => x.AZNBalance).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(x => x.TRYBalance).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(x => x.USDBalance).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(x => x.GenderId).IsRequired();
        builder.Property(x => x.CitizenshipId).IsRequired();
        builder.Property(x => x.UserPositionId).IsRequired();
        builder.Property(x => x.WarehouseId).IsRequired();
    }
}
