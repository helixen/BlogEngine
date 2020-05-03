using System.Collections.Generic;
using System.Threading.Tasks;


namespace Api.DataAccess.Contracts.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<bool> Exist(long id);

        Task<IEnumerable<T>> GetAll();

        Task<T> Get(long id);

        Task DeleteAsync(long id);

        Task<T> Update(long id, T element);

        Task<T> Add(T element);
    }
}
