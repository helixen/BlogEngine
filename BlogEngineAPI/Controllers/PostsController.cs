using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Application.Contracts.Services;
using Api.Business.Models;


namespace BlogEngineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _service;

        public PostsController(IPostService postService)
        {
            _service = postService;
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            post.CreatedDate = DateTime.Now;
            post.Status = PostStatus.Created;
            await _service.CreatePost(post);
            return Ok(post);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPublished()
        {
            var posts = await _service.GetPublishedPosts();
            return Ok(posts);
        }
        [HttpGet("GetSubmitted")]
        public async Task<ActionResult<IEnumerable<Post>>> GetSubmitted()
        {
            var posts = await _service.GetSubmittedPosts();
            return Ok(posts);
        }

        [HttpGet("GetCreated/{userid:long}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetCreated(long userid)
        {
            var posts = await _service.GetCreatedPosts(userid);
            return Ok(posts);
        }
        [HttpGet("GetRejected/{userid:long}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetRejected(long userid)
        {
            var posts = await _service.GetRejectedPosts(userid);
            return Ok(posts);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(long id)
        {
            var post = await _service.Get(id);
            return Ok(post);
        }

        [HttpGet("Submit/{id:long}")]
        public async Task<IActionResult> Submit(long id)
        {
            await _service.UpdateStatus(id, PostStatus.Submitted);
            return Ok();
        }

        [HttpGet("Approve/{id:long}/{editorId:long}")]
        public async Task<IActionResult> Approve(long id, long editorId)
        {
            await _service.UpdateStatus(id, PostStatus.Approved, editorId);
            return Ok();
        }

        [HttpGet("Reject/{id:long}/{editorId:long}")]
        public async Task<IActionResult> Reject(long id, long editorId)
        {
            await _service.UpdateStatus(id, PostStatus.Rejected, editorId);
            return Ok();
        }

        [HttpDelete("{id:long}/{editorId:long}")]
        public async Task<IActionResult> Delete(long id, long editorId)
        {
            await _service.Delete(id, editorId);
            return Ok();
        }
    }
}
