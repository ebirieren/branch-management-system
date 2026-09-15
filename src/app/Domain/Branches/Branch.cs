using Domain.Invoices;
using Domain.Products;

namespace Domain.Branches;

public class Branch
{
    public Guid RowGuid { get; set; } = Guid.NewGuid();
    public int Id { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public Guid ProductId { get; private set; }
    public ICollection<Product?> Products { get; set; }
    public Guid InvoiceId { get; private set; }
    public ICollection<Invoice?> Invoices { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
