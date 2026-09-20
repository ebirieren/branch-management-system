namespace Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Products;

public sealed class ProductConfiguration: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.ToTable("Products");

        entity.HasKey(product => product.Id);

        entity.Property(product => product.Id)
                .IsRequired()
                .HasColumnName("id");
        
        entity.HasIndex(product => product.Id)
                .IsUnique();

        entity.Property(product => product.ProductName)
                .IsRequired()
                .HasColumnName("productName");

        entity.HasIndex(product => product.ProductName)
                .IsUnique();
        
        entity.Property(product => product.ProductPrice)
                .IsRequired()
                .HasColumnName("productPrice")
                .HasPrecision(10,2);

        entity.Property(product => product.PreviousPrice)
                .HasColumnName("previousProductPrice")
                .HasPrecision(10,2);

        entity.Property(product => product.ProductCount)
                .IsRequired()
                .HasColumnName("productCount")
                .HasDefaultValue(0);

        entity.Property(product => product.TaxRate)
                .HasColumnName("taxRate");

        entity.Property(product => product.ProductPriceWithTax)
                .HasColumnName("productPriceWithTax")
                .HasPrecision(10,2);

        entity.Property(product => product.BranchId)
                .HasColumnName("branchId");

        // Original mapping preserved. Product has a Branches collection rather
        // than a scalar Branch property, and the relationship is configured in
        // BranchConfiguration.
        // entity.Property(product => product.Branch)
        //       .HasColumnName("branch");

        entity.Property(product => product.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdAt");

        entity.Property(product => product.UpdatedAt)
                .HasColumnName("updatedAt");

    }
}
