using Domain.Branches;
using Domain.Invoices;
public class StockTransaction
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int BranchId { get; set; }
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public string ProductName { get; set; } = string.Empty;
    public string SupplierName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int TaxRate { get; set; }
    public decimal UnitPriceWithTax { get; set; }

    public DateTimeOffset TransactionDate { get; set; } = DateTimeOffset.UtcNow;

    public Guid? InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
    
    public bool IsInvoiced => InvoiceId.HasValue;

    public decimal TotalExpense => Quantity * UnitPriceWithTax;
}