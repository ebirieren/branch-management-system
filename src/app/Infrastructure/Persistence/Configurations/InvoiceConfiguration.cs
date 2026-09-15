namespace Infrastructure.Persistence.Configurations;

public sealed class InvoiceConfiguration: IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> entity)
    {
        entity.ToTable("Invoices");

        entity.HasKey(invoice => invoice.RowGuid);

        entity.Property(invoice => invoice.RowGuid)
                .IsRequired()
                .HasColumnName("id");
        
        entity.HasIndex(invoice => invoice.RowGuid)
                .IsUnique();

        entity.Property(invoice => invoice.BranchId)
                .HasColumnName("branchId");

        entity.Property(invoice => invoice.BranchInformations)
                .HasColumnName("branchInformations");

        entity.Property(invoice => invoice.InvoiceTerm)
                .HasColumnName("term");
        
        entity.Property(invoice => invoice.InvoiceYear)
                .HasColumnName("year");

        entity.Property(invoice => invoice.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdAt");
    }
}