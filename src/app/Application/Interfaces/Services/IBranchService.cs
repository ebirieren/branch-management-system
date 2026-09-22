using Application.DTOs;
using Application.Requests;
using Application.Services;

namespace Application.Interfaces.Services;

public interface IBranchService : IService<BranchDTO, int, CreateBranchRequest, UpdateBranchRequest>
{
        Task<BranchDTO?> AddProductAsync(AddProductToBranchRequest request);
}