using Limak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Limak.Persistence.Configurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.Property(x=>x.Name).IsRequired().HasMaxLength(256);
        builder.Property(x=>x.ImagePath).IsRequired().HasMaxLength(512);
        builder.Property(x=>x.WebsitePath).IsRequired().HasMaxLength(512);
    }
}
