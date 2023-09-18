namespace CircleCI.DataService.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<bool> Add(T entity);
    Task<bool> Delete(int entityId);
}