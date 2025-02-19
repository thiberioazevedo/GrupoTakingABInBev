using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SalesItens");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id);
        builder.Property(u => u.ProductId).IsRequired();
        builder.Property(u => u.Quantity).IsRequired();
        builder.Property(u => u.UnitPrice).IsRequired();

        builder.HasOne(u => u.Sale).WithMany(u => u.SaleItemCollection).HasConstraintName("FK_SalesItens_Sales_SaleId");
        builder.HasOne(u => u.Product).WithMany(u => u.SaleItemCollection).HasConstraintName("FK_SalesItens_Products_SaleId");
    }
}
