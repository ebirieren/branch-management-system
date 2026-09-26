namespace Application.Requests;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
public record CreateInvoiceRequest
{
    public required string InvoiceName { get; init; } = string.Empty;
    public required int BranchId { get; init; }
    public Term InvoiceTerm { get; init; }
    
    [Range(2000,2100)]
    public int InvoiceYear { get; init; }
}