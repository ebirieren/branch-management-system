namespace Domain.Invoices;

using Domain.Enums;
using Domain.Branches;
public class Invoice
{
    public Guid RowGuid { get; set; } = Guid.NewGuid();
    public Guid BranchId { get; private set; }
    public Branch BranchInformations { get; set; } = null!;
    public Term InvoiceTerm { get; set; }
    public DateTime InvoiceYear { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
