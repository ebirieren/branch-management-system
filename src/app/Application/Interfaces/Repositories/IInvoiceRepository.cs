using Domain.Invoices;
using Domain.Enums;

namespace Application.Interfaces;

public interface IInvoiceRepository: IRepository<Invoice, Guid>
{
    Task<List<Invoice>> GetByBranchIdAsync(int branchId);

    Task<List<Invoice>> GetByInvoiceYearAsync(int branchId, int invoiceYear);

    Task<Invoice?> GetByInvoiceYearAndTermAsync(
        int branchId,
        Term term,
        int invoiceYear,
        CancellationToken cancellationToken = default);
}
