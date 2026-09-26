namespace Application.DTOs;

using Domain.Products;
using Domain.Branches;
using Domain.Invoices;
using Domain.Enums;

public record InvoiceDTO
{
    public Guid RowGuid { get; init; }
    public BranchDTO BranchInformations { get; init; } = null!;
    public Term InvoiceTerm { get; init; }
    public int InvoiceYear { get; init; }
    public DateTime CreatedAt { get; init; }
}
