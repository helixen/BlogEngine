using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Contracts.Services;
using Api.Business.Models;
using Api.DataAccess.Contracts.Repositories;
using Api.DataAccess.Mappers;
using System;

namespace Api.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        //Si deseo un objeto diferente al mio propio debo de llamar el servicio del mismo, no debo de usar el repostorio de otros objetos

        public PostService(IPostRepository postRepository, ICommentService commentService, IUserService userService)
        {
            _postRepository = postRepository;
            _commentService = commentService;
            _userService = userService; 
        }

        public async Task<IEnumerable<Post>> GetPublishedPosts()
        {
            var result = await _postRepository.GetByStatus(PostMapper.Map(PostStatus.Approved));
            foreach(var obj in result)
            {
                obj.Comments = CommentMapper.Map(await _commentService.GetCommentsByPost(obj.Id));
                var Author = await _userService.Get(obj.AuhorUserId);
                obj.AuthorName = Author.UserName;
                
                if(obj.ApproverUserId != null)
                { 
                    var Editor = await _userService.Get((long)obj.ApproverUserId);
                    obj.EditorName = Editor.UserName;
                }
            }

              
            return PostMapper.Map(result);
        }
        public async Task<IEnumerable<Post>> GetRejectedPosts(long id)
        {
            var result = await _postRepository.GetByStatus(PostMapper.Map(PostStatus.Rejected), id);
            foreach (var obj in result)
            {
                if (obj.ApproverUserId != null)
                {
                    var Editor = await _userService.Get((long)obj.ApproverUserId);
                    obj.EditorName = Editor.UserName;
                }
            }
            return PostMapper.Map(result);
        }
        public async Task<IEnumerable<Post>> GetCreatedPosts(long id)
        {
            var result = await _postRepository.GetByStatus(PostMapper.Map(PostStatus.Created), id);
            return PostMapper.Map(result);
        }
        public async Task<IEnumerable<Post>> GetSubmittedPosts()
        {
            var result = await _postRepository.GetByStatus(PostMapper.Map(PostStatus.Submitted));
            foreach (var obj in result)
            {
                var Author = await _userService.Get(obj.AuhorUserId);
                obj.AuthorName = Author.UserName;
            }
            return PostMapper.Map(result);
        }

        public async Task<Post> CreatePost(Post post)
        {
            var entity = await _postRepository.Add(PostMapper.Map(post));
            return PostMapper.Map(entity);
        }

        public async Task<Post> UpdateStatus(long id, PostStatus status, long? editorId = null)
        {
            var result= Get(id);
            Post post = result.Result;

            if (post.Status == status)
                throw new Exception("Post already has the desired status!");

            switch(status)
            {
                case PostStatus.Submitted: 
                    post.SubmittedDate = DateTime.Now; 
                    break;
                case PostStatus.Approved: 
                    if (post.Status != PostStatus.Submitted)
                        throw new Exception("Post is not ready to be approved!");
                    post.PublishedDate = DateTime.Now;
                    break;
                case PostStatus.Rejected:
                    if (post.Status != PostStatus.Submitted)
                        throw new Exception("Post is not ready to be rejected!");
                    post.RejectedDate = DateTime.Now; 
                    break;
                case PostStatus.Deleted: 
                    post.DeletedDate = DateTime.Now; 
                    break;
            }
            if (editorId != null)
                post.ApproverUserId = editorId;

            post.Status = status;
            
            var entity = await _postRepository.Update(id, PostMapper.Map(post));

            return PostMapper.Map(entity);
        }

        public async Task<Post> Get(long id)
        {
            var entity = await _postRepository.Get(id);
            return PostMapper.Map(entity);
        }

        public async Task<Post> Delete(long id, long userId)
        {
            //Logic deletion to preserve data consistency.
            return await UpdateStatus(id, PostStatus.Deleted, userId);
        }

    }
}
