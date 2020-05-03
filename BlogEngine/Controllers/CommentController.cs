using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Models;
using Microsoft.Extensions.Configuration;

namespace BlogEngine.Controllers
{

    public class CommentController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        string _apiUrl;

        public CommentController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _apiUrl = _configuration["ApiUrl"];

        }

        [HttpPost]
        public ActionResult Comment(Comment comment)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies["_UserId"] != null) { 
                string AuthorUserId = _httpContextAccessor.HttpContext.Request.Cookies["_UserId"];
                comment.AuthorUserId = Convert.ToInt64(AuthorUserId);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);

                var postTask = client.PostAsJsonAsync<Comment>("comment", comment);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return RedirectToAction("Index","Home");
        }
    }
}