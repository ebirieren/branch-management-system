using Application.DTOs;
using Application.Requests;

namespace Application.Interfaces.Services;
public interface IBranchStockService
{
    Task<BranchDTO?> AddProductAsync(AddProductToBranchRequest request);
    
}