using Limak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Limak.Persistence.Configurations;

public class KargomatConfiguration : IEntityTypeConfiguration<Kargomat>
{
    public void Configure(EntityTypeBuilder<Kargomat> builder)
    {
        builder.Property(x=>x.Location).IsRequired().HasMaxLength(256);   
        builder.Property(x=>x.CordinateX).IsRequired().HasMaxLength(256);   
        builder.Property(x=>x.CordinateY).IsRequired().HasMaxLength(256);   
        builder.Property(x=>x.Price).IsRequired().HasColumnType("decimal(18,2)");   
    }
}
