using Microsoft.EntityFrameworkCore;
using Domain.Branches;
using Domain.Invoices;
using Domain.Products;
using Domain.User;
using Domain.Transactions;
using Infrastructure;

namespace Infrastructure;

public class AppDBContext: DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
    {
        
    }

    public DbSet<Branch> Branches => Set<Branch>();

    public DbSet<Invoice> Invoices => Set<Invoice>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<BranchStock> BranchStock => Set<BranchStock>();

    public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDBContext).Assembly
        );
    }
    
}