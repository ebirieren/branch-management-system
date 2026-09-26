using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces.Services;
using Application.Requests;

namespace API.Controllers;

[ApiController]
[Route("api/branches")]
public class BranchesController : ControllerBase
{
    private readonly IBranchService _branchService;

    public BranchesController(IBranchService branchService)
    {
        _branchService = branchService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BranchDTO>> GetById(int id)
    {
        BranchDTO? branch = await _branchService.GetByIdAsync(id);

        if (branch is null)
        {
            return NotFound();
        }
        return Ok(branch);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BranchDTO>>> GetAll()
    {
        IReadOnlyList<BranchDTO> branches = await _branchService.GetAllAsync();

        if (branches is null)
        {
            return NotFound();
        }
        
        return Ok(branches);
    }

    [HttpPost("create")]
    public async Task<ActionResult<BranchDTO>> Create(CreateBranchRequest request)
    {
        BranchDTO branch = await _branchService.CreateAsync(request);

        return Ok(branch);
    }

    [HttpPost("update")]
    public async Task<ActionResult<BranchDTO>> Update(UpdateBranchRequest request)
    {
        BranchDTO branch = await _branchService.UpdateAsync(request);

        return Ok(branch);
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult<bool>> Remove(int id)
    {
        bool result = await _branchService.DeleteAsync(id);

        if (result != true)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("addProduct")]
    public async Task<ActionResult<BranchDTO?>> AddProduct(AddProductToBranchRequest request)
    {
        BranchDTO? branch = await _branchService.AddProductAsync(request);

        if (branch is null)
        {
            return NotFound();
        }

        return Ok(branch);
    }
}