using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Products;
using Domain.Branches;

namespace Infrastructure.Persistence.Configurations;
public sealed class BranchStockConfiguration: IEntityTypeConfiguration<BranchStock>
{
    public void Configure(EntityTypeBuilder<BranchStock> entity)
    {
        entity.ToTable("BranchStock");

        entity.HasKey(branchStock => new { branchStock.BranchId, branchStock.ProductId});

        entity.Property(branchStock => branchStock.BranchId)
                .IsRequired()
                .HasColumnName("branchId");

        entity.Property(branchStock => branchStock.ProductId)
                .HasColumnName("productId");

        entity.Property(branchStock => branchStock.ProductCount)
                .IsRequired()
                .HasColumnName("productCount");

        entity.HasOne(branchStock => branchStock.Branch)
                .WithMany(branch => branch.BranchStocks)
                .HasForeignKey(branchStock => branchStock.BranchId);

        entity.HasOne(branchStock => branchStock.Product)
                .WithMany(product => product.BranchStocks)
                .HasForeignKey(branchStock => branchStock.ProductId);


    }
}