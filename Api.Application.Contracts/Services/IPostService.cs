using System;

using System.Threading.Tasks;
using Api.Business.Models;
using System.Collections.Generic;

namespace Api.Application.Contracts.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPublishedPosts();
        Task<IEnumerable<Post>> GetSubmittedPosts();
        Task<IEnumerable<Post>> GetRejectedPosts(long id);
        Task<IEnumerable<Post>> GetCreatedPosts(long id);
        Task<Post> CreatePost(Post post);
        Task<Post> UpdateStatus(long id, PostStatus status, long? editorId=null);
        Task<Post> Get(long id);
        Task<Post> Delete(long id, long userId);
    }
}
