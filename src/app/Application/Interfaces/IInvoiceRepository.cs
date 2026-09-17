using Application.Interfaces;

namespace Application.Interfaces;

public interface IInvoiceRepository: IRepository<Invoice>
{
    Task<List<Invoice?>> GetByBranchIdAsync(Guid branchId);

    Task<List<Invoice?>> GetByInvoiceYearAsync(Guid branchId, Datetime invoiceYear);

    Task<Invoice?> GetByInvoiceYearAndTermAsync(Guid branchId, Term term, Datetime invoiceYear);
}