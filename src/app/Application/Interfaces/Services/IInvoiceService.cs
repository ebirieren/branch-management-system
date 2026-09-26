using Application.DTOs;
using Application.Requests;
using Application.Services;

namespace Application.Interfaces.Services;

public interface IInvoiceService : IService<InvoiceDTO, Guid, CreateInvoiceRequest, UpdateInvoiceRequest>
{

}