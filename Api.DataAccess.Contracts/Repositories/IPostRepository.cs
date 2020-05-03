using System.Collections.Generic;
using Api.DataAccess.Contracts.Entities;
using System.Threading.Tasks;


namespace Api.DataAccess.Contracts.Repositories
{
    public interface IPostRepository : IRepository<PostEntity>
    {
        Task<PostEntity> Update(PostEntity postEntity);
        Task<IEnumerable<PostEntity>> GetByStatus(PostStatus status);
        Task<IEnumerable<PostEntity>> GetByStatus(PostStatus status, long id);
    }
}
