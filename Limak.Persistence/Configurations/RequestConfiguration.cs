using Limak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Limak.Persistence.Configurations;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.Property(x => x.CountryId).IsRequired();
        builder.Property(x => x.RequestSubjectId).IsRequired();
        builder.Property(x => x.Status).HasDefaultValue(false);
    }
}
