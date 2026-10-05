using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces.Services;

namespace API.Controllers;

[ApiController]
[Route("api/transactions")]
public class StockTransactionController : ControllerBase
{
    private readonly IStockTransactionService _stockTransactionService;

    public StockTransactionController(IStockTransactionService stockTransactionServive)
    {
        _stockTransactionService = stockTransactionServive;
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<StockTransactionDTO>> GetById(Guid id)
    {
        StockTransactionDTO transaction = await _stockTransactionService.GetByIdAsync(id);

        if (transaction is null)
        {
            return NotFound();
        }

        return Ok(transaction);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<StockTransactionDTO>>> GetAll()
    {
        IReadOnlyList<StockTransactionDTO> transactions = await _stockTransactionService.GetAllAsync();

        return Ok(transactions);
    }
}