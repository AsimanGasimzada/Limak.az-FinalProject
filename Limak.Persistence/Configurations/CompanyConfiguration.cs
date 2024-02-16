using Limak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Limak.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.Property(x => x.AZNBalance).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(x => x.TRYBalance).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(x => x.USDBalance).HasColumnType("decimal(18,2)").HasDefaultValue(0);
    }
}
