namespace Domain.Invoices;

using Domain.Enums;
using Domain.Branches;
public class Invoice
{
    public Guid RowGuid { get; set; } = Guid.NewGuid();
    public string InvoiceName { get; set; } = string.Empty;
    public int BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
    public ICollection<StockTransaction> Transactions { get; set; } = new List<StockTransaction>();
    public Term InvoiceTerm { get; set; }
    public int InvoiceYear { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
