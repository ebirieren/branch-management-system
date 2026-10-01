using Domain.Branches;
using Domain.Products;
using Domain.Transactions;

namespace Domain.Branches;
public class BranchStock
{
    public int BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int ProductCount { get; set; }
}