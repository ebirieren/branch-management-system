namespace Application.Requests;

public record UpdateInvoiceRequest
{
    public required Guid InvoiceId { get; set; }
    public required string InvoiceName { get; set; } = string.Empty;
}