namespace Application.DTOs;

using Domain.Products;
using Domain.Branches;
using Domain.Invoices;
using Domain.Enums;

public record InvoiceDTO
{
    public Guid RowGuid { get; init; }
    public Guid BranchId { get; init;}
    public Branch BranchInformations { get; init; } = null!;
    public Term InvoiceTerm { get; init; }
    public DateTime InvoiceYear { get; init; }
    public DateTime CreatedAt { get; init; }
}
