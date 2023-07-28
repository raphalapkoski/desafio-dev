using DesafioDev.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDev.Infra.Persistence.Mapping;

public sealed class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Type)
               .IsRequired();

        builder.Property(_ => _.Date)
               .IsRequired();

        builder.Property(_ => _.Value)
               .IsRequired();

        builder.Property(_ => _.Card)
               .IsRequired();

        builder.Property(_ => _.Hour)
               .IsRequired();
        
        builder.ToTable("Transactions");
    }
}
