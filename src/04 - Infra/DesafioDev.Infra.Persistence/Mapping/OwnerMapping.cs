using DesafioDev.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioDev.Infra.Persistence.Mapping;

public class OwnerMapping : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.HasKey(_ => _.Id);

        builder.Property(_ => _.Name)
               .IsRequired();

        builder.Property(_ => _.Cpf)
               .IsRequired();

        builder.ToTable("Owners");
    }
}
