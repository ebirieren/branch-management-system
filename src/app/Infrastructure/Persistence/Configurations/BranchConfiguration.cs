
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

            entity.Property(branch => branch.InvoiceId)
                    .HasColumnName("invoiceId");

            entity.Property(branch => branch.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnName("createdAt");

            entity.Property(branch => branch.UpdatedAt)
                    .HasColumnName("updatedAt");

            entity.HasMany(branch => branch.Products)
                    .WithMany(product => product.Branches);

            entity.HasMany(branch => branch.Invoices)
                    .WithOne(invoice => invoice.BranchInformations)
                    .HasForeignKey(invoice => invoice.BranchId);
    }
}
