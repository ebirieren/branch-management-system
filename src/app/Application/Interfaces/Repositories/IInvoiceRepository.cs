using Domain.Invoices;
using Domain.Enums;

namespace Application.Interfaces;

public interface IInvoiceRepository: IRepository<Invoice, Guid>
{
    Task<List<Invoice>> GetByBranchIdAsync(Guid branchId);

    Task<List<Invoice>> GetByInvoiceYearAsync(Guid branchId, DateTime invoiceYear);

    Task<Invoice?> GetByInvoiceYearAndTermAsync(
        Guid branchId,
        Term term,
        DateTime invoiceYear,
        CancellationToken cancellationToken = default);
}
