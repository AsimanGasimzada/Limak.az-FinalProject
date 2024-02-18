using Limak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Limak.Persistence.Configurations;

public class RequestSubjectConfiguration : IEntityTypeConfiguration<RequestSubject>
{
    public void Configure(EntityTypeBuilder<RequestSubject> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
    }
}
