using System;

using System.Threading.Tasks;
using Api.Business.Models;
using System.Collections.Generic;

namespace Api.Application.Contracts.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsByPost(long postId);
        Task<Comment> CreateComment(Comment Comment);
    }
}
