using Domain.Branches;

namespace Domain.Products;

public class Product 
{
    public Guid RowGuid { get; set; } = Guid.NewGuid();
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; } = decimal.Empty;
    public decimal? PreviousPrice { get; set; }
    public int ProductCount { get; set; } = int.Empty;
    public int? TaxRate { get; set; }
    public decimal? ProductPriceWithTax { get; set; }
    public Guid BranchId { get; private set;}
    public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}