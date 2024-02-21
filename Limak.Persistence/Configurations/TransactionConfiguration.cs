using Limak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Limak.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(x => x.AppUserId).IsRequired();
        builder.Property(x => x.Feedback).IsRequired().HasMaxLength(4096);
        builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.UserBalance).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.IsPayment).IsRequired();
    }
}
