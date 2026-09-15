using Microsoft.EntityFrameworkCore;
using Domain.Branches;
using Domain.Invoices;
using Domain.Products;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDBContext).Assembly
        );
    }
    
}