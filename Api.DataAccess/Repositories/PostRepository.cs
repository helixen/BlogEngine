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

        public async Task<PostEntity> Update(PostEntity postEntity)
        {
            var objecExists = await Exist(postEntity.Id);
            if (objecExists)
            {
                var updateEntity = _apiDBContext.Posts.Update(postEntity);
                await _apiDBContext.SaveChangesAsync();
                return updateEntity.Entity;
            }
            else
                throw new Exception("Object not found");
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

        public async Task<PostEntity> Get(long idEntity)
        {
            var result = await _apiDBContext.Posts.FirstOrDefaultAsync(x => x.Id == idEntity);
            if (result == null)
                throw new Exception("Object not found");

            return result;
        }

        public async Task DeleteAsync(long idEntity)
        {
            var entity = await _apiDBContext.Posts.SingleAsync(x => x.Id == idEntity);

            var result = _apiDBContext.Posts.Remove(entity);

            await _apiDBContext.SaveChangesAsync();
        }

        public Task<bool> Exist(long id)
        {
            var result = _apiDBContext.Posts.Where(x => x.Id.Equals(id));
            bool answer = true;
            if (result == null)
                answer = false;
            return Task.FromResult(answer);
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

        public Task<PostEntity> Update(int id, PostEntity element)
        {
            throw new NotImplementedException();
        }
    }
}
