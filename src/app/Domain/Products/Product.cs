using Domain.Branches;

namespace Domain.Products;

public class Product 
{
    public Guid RowGuid { get; set; } = Guid.NewGuid();
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public decimal? PreviousPrice { get; set; }
    public int ProductCount { get; set; }
    public int? TaxRate { get; set; }
    public decimal? ProductPriceWithTax { get; set; }
    public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}