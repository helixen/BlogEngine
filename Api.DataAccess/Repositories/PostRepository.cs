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
    public class PostRepository : IPostRepository
    {
        private readonly IApiDBContext _apiDBContext;

        public PostRepository(IApiDBContext apiDBContext)
        {
            _apiDBContext = apiDBContext;
        }

        public async Task<PostEntity> Add(PostEntity userEntity)
        {
            await _apiDBContext.Posts.AddAsync(userEntity);

            await _apiDBContext.SaveChangesAsync();

            return userEntity;
        }

        public async Task<PostEntity> Update(long id, PostEntity postEntity)
        {
            var result = await _apiDBContext.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
                throw new Exception("Object not found");

            result.Content = postEntity.Content;
            result.Status = postEntity.Status;
            result.AuhorUserId = postEntity.AuhorUserId;
            result.ApproverUserId = postEntity.ApproverUserId;
            result.CreatedDate = postEntity.CreatedDate;
            result.SubmittedDate = postEntity.SubmittedDate;
            result.PublishedDate = postEntity.PublishedDate;
            result.RejectedDate = postEntity.RejectedDate;
            result.DeletedDate = postEntity.DeletedDate;
            await _apiDBContext.SaveChangesAsync();
            return result;
        }

        public async Task<PostEntity> Update(PostEntity postEntity)
        {
            if (await Exist(postEntity.Id))
            {
                var updateEntity = _apiDBContext.Posts.Update(postEntity);
                await _apiDBContext.SaveChangesAsync();
                return updateEntity.Entity;
            }
            else
            {
                throw new Exception("Object not found");
            }
        }

        public async Task<PostEntity> Get(long idEntity)
        {
            var result = await _apiDBContext.Posts.FirstOrDefaultAsync(x => x.Id == idEntity);
            return result ?? throw new Exception("Object not found");
        }

        public async Task DeleteAsync(long idEntity)
        {
            var entity = await _apiDBContext.Posts.SingleAsync(x => x.Id == idEntity);

            var result = _apiDBContext.Posts.Remove(entity);

            await _apiDBContext.SaveChangesAsync();
        }

        public async Task<bool> Exist(long id)
        {
            return await _apiDBContext.Posts.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<PostEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PostEntity>> GetByStatus(PostStatus status)
        {
            var result = _apiDBContext.Posts.Where(x => x.Status.Equals(status))
                .Include(x => x.Comments).OrderByDescending(x => x.CreatedDate);

            return await result.ToListAsync();
        }
        public async Task<IEnumerable<PostEntity>> GetByStatus(PostStatus status, long userId)
        {
            var result = _apiDBContext.Posts.Where(x => x.Status.Equals(status) && x.AuhorUserId.Equals(userId)).OrderByDescending(x => x.CreatedDate);
            return await result.ToListAsync();
        }

    }
}
