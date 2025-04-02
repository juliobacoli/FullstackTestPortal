using FullstackTest.Api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FullstackTest.Api.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Quantity)
            .IsRequired();

        builder.Property(i => i.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasOne(i => i.Product)
               .WithMany()
               .HasForeignKey(i => i.ProductId);
    }
}
