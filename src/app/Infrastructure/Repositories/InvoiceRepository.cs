namespace Infrastructure.Repositories;

public class InvoiceRepository: Repository<Invoice>, IInvoiceRepository
{
    private readonly AppDBContext _context;

    public InvoiceRepository(AppDBContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<Invoice?>> GetByIdAsync(Guid id)
    {
        return _context.Invoices
            /*.FirstOrDefaultAsync(
                invoice => invoice.RowGuid == id, 
                cancellationToken
            );*/
            .Where(invoices => invoice.RowGuid == id)
            //.FirstOrDefaultAsync(cancellationToken);
            .ToListAsync();
    }

    public Task<List<Invoice?>> GetAllAsync()
    {
        return _context.Invoices
            .ToListAsync();
    }

    public Task AddAsync(Invoice invoice)
    {
        return _context.Invoices
            .AddAsync(invoice);
    }

    public void Update(Invoice invoice)
    {
        return _context.Invoices
            .Update(invoice);
    }

    public void Remove(Invoice invoice)
    {
        return _context.Invoices
            .Remove(invoice);
    }

    public Task<List<Invoice>> GetByBranchIdAsync(Guid branchId)
    {
        return _context.Invoices
            .Where(invoice => invoice.BranchId == branchId)
            .ToListAsync();
    }

    public Task<List<Invoice>> GetByInvoiceYearAsync(Guid branchId, Datetime invoiceYear)
    {
        return _context.Invoices
            .Where(invoice =>   invoice.BranchId == branchId &&
                                invoice.InvoiceYear == invoiceYear)
            .ToListAsync();
    }

    public Task<Invoice> GetByInvoiceYearAndTermAsync(Guid branchId, Term term, Datetime invoiceYear, CancellationToken cancellationToken = default)
    {
        return _context.Invoices
            .Where(invoice =>   invoice.BranchId == branchId &&
                                invoice.InvoiceTerm == term &&
                                invoice.InvoiceYear == invoiceYear)
            .SingleOrDefaultAsync(cancellationToken);
    }
}