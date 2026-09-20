namespace Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Domain.Invoices;
using Domain.Enums;
using Application.Interfaces;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDBContext _context;

    public InvoiceRepository(AppDBContext context)
    {
        _context = context;
    }

    public Task<Invoice?> GetByIdAsync(Guid id)
    {
        return _context.Invoices
            .FirstOrDefaultAsync(invoice => invoice.RowGuid == id);
    }

    public Task<List<Invoice>> GetAllAsync()
    {
        return _context.Invoices
            .ToListAsync();
    }

    public async Task AddAsync(Invoice invoice)
    {
        await _context.Invoices.AddAsync(invoice);
        await _context.SaveChangesAsync();
    }

    public void Update(Invoice invoice)
    {
        _context.Invoices.Update(invoice);
        _context.SaveChanges();
    }

    public void Remove(Invoice invoice)
    {
        _context.Invoices.Remove(invoice);
        _context.SaveChanges();
    }

    public Task<List<Invoice>> GetByBranchIdAsync(Guid branchId)
    {
        return _context.Invoices
            .Where(invoice => invoice.BranchId == branchId)
            .ToListAsync();
    }

    public Task<List<Invoice>> GetByInvoiceYearAsync(Guid branchId, DateTime invoiceYear)
    {
        return _context.Invoices
            .Where(invoice =>   invoice.BranchId == branchId &&
                                invoice.InvoiceYear == invoiceYear)
            .ToListAsync();
    }

    public Task<Invoice?> GetByInvoiceYearAndTermAsync(
        Guid branchId,
        Term term,
        DateTime invoiceYear,
        CancellationToken cancellationToken = default)
    {
        return _context.Invoices
            .Where(invoice =>   invoice.BranchId == branchId &&
                                invoice.InvoiceTerm == term &&
                                invoice.InvoiceYear == invoiceYear)
            .SingleOrDefaultAsync(cancellationToken);
    }
}
