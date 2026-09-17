namespace Application.Interfaces;

public interface IRepository<TEntity> where TEntity: class
{
    Task<List<TEntity?>> GetByIdAsync(TId id);
    
    Task<List<TEntity?>> GetAllAsync();

    Task AddAsync(TEntity entity);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}