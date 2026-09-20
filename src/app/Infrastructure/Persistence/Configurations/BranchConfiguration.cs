
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domain.Products;
using Domain.Branches;

namespace Infrastructure.Persistence.Configurations;

public sealed class BranchConfiguration: IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> entity)
    {
           entity.ToTable("Branches");

           entity.HasKey(branch => branch.Id);

           entity.Property(branch => branch.Id)
                    .IsRequired()
                    .HasColumnName("id");

            entity.HasIndex(branch => branch.Id)
                    .IsUnique();

           entity.Property(branch => branch.BranchName)
                    .HasMaxLength(100)
                    .IsRequired()
                    .HasColumnName("branchName");

            entity.HasIndex(branch => branch.BranchName)
                    .IsUnique();

            entity.Property(branch => branch.ProductId)
                    .HasColumnName("productId");

            // Original mapping preserved: Products is a navigation collection,
            // so EF configures it through HasMany instead of Property.
            // entity.Property(branch => branch.Products)
            //       .HasColumnName("product");

            entity.Property(branch => branch.InvoiceId)
                    .HasColumnName("invoiceId");

            // Original mapping preserved: Invoices is a navigation collection,
            // so EF configures it through HasMany instead of Property.
            // entity.Property(branch => branch.Invoices)
            //       .HasColumnName("invoice");

            entity.Property(branch => branch.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnName("createdAt");

            entity.Property(branch => branch.UpdatedAt)
                    .HasColumnName("updatedAt");

            entity.HasMany(branch => branch.Products)
                    .WithMany(product => product.Branches);

            // The original HasForeignKey(product => product.BranchId) cannot be
            // attached directly to a many-to-many relationship in EF Core.

            entity.HasMany(branch => branch.Invoices)
                    .WithOne(invoice => invoice.BranchInformations)
                    .HasForeignKey(invoice => invoice.BranchId)
                    .HasPrincipalKey(branch => branch.RowGuid);
    }
}
