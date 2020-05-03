using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Application.Contracts.Services;
using Api.Business.Models;

namespace BlogEngineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService commentService)
        {
            _service = commentService;
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> CreatePost(Comment comment)
        {
            comment.CreatedDate = DateTime.Now;
            await _service.CreateComment(comment);
            return Ok(comment);
        }
    }
}
