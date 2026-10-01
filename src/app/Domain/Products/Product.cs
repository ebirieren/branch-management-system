using Domain.Branches;

namespace Domain.Products;

public class Product 
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public decimal? PreviousPrice { get; set; }
    public int ProductCount { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public int? TaxRate { get; set; }
    public decimal? ProductPriceWithTax => ProductPrice * TaxRate / 100  + ProductPrice;
    public decimal? InitialTotalCost => ProductCount * ProductPriceWithTax;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}