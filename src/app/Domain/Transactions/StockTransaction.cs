using Domain.Branches;
using Domain.Invoices;
using Domain.Products;
namespace Domain.Transactions;
public class StockTransaction
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int BranchId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public string ProductNameAtTransaction { get; set; } = string.Empty;
    public string SupplierNameAtTransaction { get; set; } = string.Empty;
    public decimal ProductPriceAtTransaction { get; set; }
    public int? TaxRateAtTransaction { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPriceWithTax { get; set; }

    public DateTimeOffset TransactionDate { get; set; } = DateTimeOffset.UtcNow;

    public Guid? InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
    
    public bool IsInvoiced => InvoiceId.HasValue;

    public decimal TotalExpense => Quantity * UnitPriceWithTax;
}