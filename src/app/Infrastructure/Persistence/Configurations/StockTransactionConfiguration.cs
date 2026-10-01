namespace Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Transactions;

public sealed class StockTransactionConfiguration: IEntityTypeConfiguration<StockTransaction>
{
    public void Configure(EntityTypeBuilder<StockTransaction> entity)
    {
        entity.ToTable("StockTransactions");

        entity.HasKey(transaction => transaction.Id);

        entity.Property(transaction => transaction.Id)
            .IsRequired()
            .HasColumnName("id");

        entity.HasIndex(transaction => transaction.Id)
            .IsUnique();

        entity.Property(transaction => transaction.BranchId)
            .HasColumnName("branchId");

        entity.Property(transaction => transaction.ProductId)
            .HasColumnName("ProductId");

        entity.Property(transaction => transaction.Quantity)
            .HasColumnName("productQuantity");

        entity.Property(transaction => transaction.TransactionDate)
            .HasColumnName("transactionDate");
        
        entity.Property(transaction => transaction.InvoiceId)
            .HasColumnName("invoiceId");

        entity.Property(transaction => transaction.IsInvoiced)
            .HasColumnName("isInvoiced");

        entity.HasOne(stockTransaction => stockTransaction.Product)
            .WithMany()
            .HasForeignKey(stockTransaction => stockTransaction.ProductId);

        entity.HasOne(stockTransaction => stockTransaction.Invoice)
            .WithMany(invoice => invoice.Transactions)
            .HasForeignKey(stockTransaction => stockTransaction.InvoiceId)
            .IsRequired(false);
        
        entity.Ignore(transaction => transaction.IsInvoiced);

    }
}