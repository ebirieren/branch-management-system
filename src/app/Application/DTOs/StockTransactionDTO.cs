namespace Application.DTOs;
using Application.DTOs;

public record StockTransactionDTO
{
    public Guid Id { get; init; }
    public int BranchId { get; init; }
    public ProductTransactionDTO Product {get; init; } = new ProductTransactionDTO();
    public int Quantity { get; init; }
    public DateTimeOffset TransactionDate { get; init; } = DateTime.UtcNow;
    public decimal UnitPriceWithTax { get; init; }
    public decimal TotalExpense { get; init; }
    public Guid? InvoiceId { get; init; }
    public bool IsInvoiced { get; init; }
}
    
