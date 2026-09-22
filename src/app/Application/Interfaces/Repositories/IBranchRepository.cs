using Application.DTOs;
using Domain.Branches;
namespace Application.Interfaces;

public interface IBranchRepository: IRepository<Branch, int>
{
}