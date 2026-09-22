namespace Application.Services;

public interface IService<TDTO, TId, TCreateRequest, TUpdateRequest> where TDTO: class
{
    Task<TDTO?> GetByIdAsync(TId id);

    Task<IReadOnlyList<TDTO?>> GetAllAsync();

    Task<TDTO> CreateAsync(TCreateRequest request);

    Task<TDTO?> UpdateAsync(TId id, TUpdateRequest request);

    Task<bool> DeleteAsync(TId id);
    
}