
public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int BranchId { get; set; }
    public Branch Branch { get; set; } = null;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null;

    public int Quantity { get; set; }
    public DateTimeOffset TransactionDate { get; set; } = DateTimeOffset.UtcNow;

    public Guid? InvoiceId { get; set; }

    public Invoice? Invoice { get; set; }

    public bool IsInvoiced => InvoiceId.HasValue;
}