using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.DataAccess.Contracts;
using Api.DataAccess.Contracts.Entities;
using Api.DataAccess.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IApiDBContext _apiDBContext;

        public CommentRepository(IApiDBContext apiDBContext)
        {
            _apiDBContext = apiDBContext;
        }

        public async Task<CommentEntity> Add(CommentEntity commentEntity)
        {
            await _apiDBContext.Comments.AddAsync(commentEntity);
            await _apiDBContext.SaveChangesAsync();
            return commentEntity;
        }
        public async Task<IEnumerable<CommentEntity>> GetByPost(long postId)
        {
            var result = _apiDBContext.Comments.Where(x => x.PostId.Equals(postId)).OrderByDescending(x => x.CreatedDate);
            return await result.ToListAsync();
        }
        public async Task<CommentEntity> Update(long id, CommentEntity element)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Exist(long id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<CommentEntity>> GetAll()
        {
            throw new NotImplementedException();
        }
        public async Task<CommentEntity> Get(long id)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
