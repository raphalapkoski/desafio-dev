using DesafioDev.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDev.Infra.Persistence.Mapping;

public sealed class EstablishmentMapping : IEntityTypeConfiguration<Establishment>
{
    public void Configure(EntityTypeBuilder<Establishment> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Name)
               .IsRequired();

        builder.HasOne(_ => _.Owner)
               .WithOne(_ => _.Establishment)
               .HasForeignKey<Establishment>("OwnerId")
               .IsRequired();

        builder.HasMany(_ => _.Transactions)
               .WithOne(_ => _.Establishment)                   
               .HasForeignKey("EstablishmentId")
               .IsRequired();

        builder.ToTable("Establishments");
    }
}
