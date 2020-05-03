using System.Collections.Generic;
using Api.DataAccess.Contracts.Entities;
using System.Threading.Tasks;


namespace Api.DataAccess.Contracts.Repositories
{
    public interface ICommentRepository : IRepository<CommentEntity>
    {
        Task<IEnumerable<CommentEntity>> GetByPost(long postId);
    }
}
