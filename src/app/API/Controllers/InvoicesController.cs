using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces.Services;
using Application.Requests;

namespace API.Controllers;

[ApiController]
[Route("api/invoices")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<InvoiceDTO>> GetById(Guid id)
    {
        InvoiceDTO invoice = await _invoiceService.GetByIdAsync(id);

        if (invoice is null)
        {
            return NotFound();
        }

        return Ok(invoice);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<InvoiceDTO>>> GetAll()
    {
        IReadOnlyList<InvoiceDTO> invoices = await _invoiceService.GetAllAsync();

        if (invoices is null)
        {
            return NotFound();
        }

        return Ok(invoices);
    }

    [HttpPost("create")]
    public async Task<ActionResult<InvoiceDTO>> Create(CreateInvoiceRequest request)
    {
        InvoiceDTO invoice = await _invoiceService.CreateAsync(request);

        return Ok(invoice);
    }

    [HttpPost("update")]
    public async Task<ActionResult<InvoiceDTO>> Update(UpdateInvoiceRequest request)
    {
        InvoiceDTO invoice = await _invoiceService.UpdateAsync(request);

        return Ok(invoice);
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult<bool>> Delete(Guid id)
    {
        bool result = await _invoiceService.DeleteAsync(id);

        if (result != true)
        {
            return NotFound();
        }

        return Ok(result);
    }
}