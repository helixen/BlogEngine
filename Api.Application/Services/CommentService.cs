using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Contracts.Services;
using Api.Business.Models;
using Api.DataAccess.Contracts.Repositories;
using Api.DataAccess.Mappers;
using System;

namespace Api.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserService _userService;  

        //Si deseo un objeto diferente al mio propio debo de llamar el servicio del mismo, no debo de usar el reCommentorio de otros objetos

        public CommentService(ICommentRepository commentRepository, IUserService userService)
        {
            _commentRepository = commentRepository;
            _userService = userService;
        }
        public async Task<IEnumerable<Comment>> GetCommentsByPost(long postId)
        {
            var result = await _commentRepository.GetByPost(postId);
            foreach (var obj in result)
            {
                if (obj.AuthorUserId != null)
                {
                    var Author = await _userService.Get((long)obj.AuthorUserId);
                    obj.AuthorName = Author.UserName;
                }
                else
                    obj.AuthorName = "anonymous";
            }
            return CommentMapper.Map(result);
        }

        public async Task<Comment> CreateComment(Comment Comment)
        {
            var entity = await _commentRepository.Add(CommentMapper.Map(Comment));
            return CommentMapper.Map(entity);
        }       
    }
}
