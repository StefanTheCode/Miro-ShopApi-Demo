using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApi.Domain.Entities;

namespace ShopApi.Infrastructure.Persistence.Configurations;

public class DiscountCodeConfiguration : IEntityTypeConfiguration<DiscountCode>
{
    public void Configure(EntityTypeBuilder<DiscountCode> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Code).IsRequired().HasMaxLength(50);
        builder.HasIndex(d => d.Code).IsUnique();
        builder.Property(d => d.Percentage).HasColumnType("decimal(5,2)");
    }
}
