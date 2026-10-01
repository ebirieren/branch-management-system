using Domain.Invoices;
using Domain.Products;

namespace Domain.Branches;

public class Branch
{
    public int Id { get; set; }
    public string BranchName { get; set; } = string.Empty;

    public ICollection<BranchStock> BranchStocks { get; set; } = new List<BranchStock>();

    public ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();

    public Guid InvoiceId { get; private set; }
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
