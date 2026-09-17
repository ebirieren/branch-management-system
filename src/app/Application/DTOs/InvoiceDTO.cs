namespace Application.DTOs;

public record Invoice
{
    public Guid RowGuid { get; init; }
    public Guid BranchId { get; init;}
    public Branch BranchInformations { get; init; }
    public Term InvoiceTerm { get; init; }
    public DateTime InvoiceYear { get; init; }
    public DateTime CreatedAt { get; init; }
}