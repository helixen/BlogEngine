using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BlogEngine.Models;
using System.Net.Http;
using Models;
using Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


namespace BlogEngine.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        string _apiUrl;
        public HomeController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _apiUrl = _configuration["ApiUrl"];
        }

        public IActionResult Index()
        {
            IEnumerable<Post> posts = null;
            

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);

                var responseTask = client.GetAsync("Posts");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Post>>();
                    readTask.Wait();

                    posts = readTask.Result;
                }
                else
                {
                    //Error response received   
                    posts = Enumerable.Empty<Post>();
                    ModelState.AddModelError(string.Empty, "Server error.");
                }
            }
            return View(posts);
        }

        public IActionResult Create()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserRole] == UserRole.Writer.ToString())
                return View();
            else
                return RedirectToAction("Index");
        }

        public IActionResult GetCreated()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserRole] == UserRole.Writer.ToString())
            {
                string AuthorUserId = _httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserId];

                IEnumerable<Post> posts = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiUrl);

                    //Called Member default GET All records  
                    //GetAsync to send a GET request   
                    // PutAsync to send a PUT request  
                    var responseTask = client.GetAsync("Posts/GetCreated/" + AuthorUserId);
                    responseTask.Wait();

                    //To store result of web api response.   
                    var result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Post>>();
                        readTask.Wait();

                        posts = readTask.Result;
                    }
                    else
                    {
                        //Error response received   
                        posts = Enumerable.Empty<Post>();
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                return View(posts);
            }
            else
                return RedirectToAction("Index");

        }

        public IActionResult GetSubmitted()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserRole] == UserRole.Editor.ToString())
            {
                IEnumerable<Post> posts = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiUrl);

                    //Called Member default GET All records  
                    //GetAsync to send a GET request   
                    // PutAsync to send a PUT request  
                    var responseTask = client.GetAsync("Posts/GetSubmitted");
                    responseTask.Wait();

                    //To store result of web api response.   
                    var result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Post>>();
                        readTask.Wait();

                        posts = readTask.Result;
                    }
                    else
                    {
                        //Error response received   
                        posts = Enumerable.Empty<Post>();
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                return View(posts);
            }
            else
                return RedirectToAction("Index");

        }

        public IActionResult GetRejected()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserRole] == UserRole.Writer.ToString())
            {
                string AuthorUserId = _httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserId];

                IEnumerable<Post> posts = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiUrl);

                    var responseTask = client.GetAsync("Posts/GetRejected/" + AuthorUserId);
                    responseTask.Wait();

                    //To store result of web api response.   
                    var result = responseTask.Result;

                    //If success received   
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Post>>();
                        readTask.Wait();

                        posts = readTask.Result;
                    }
                    else
                    {
                        //Error response received   
                        posts = Enumerable.Empty<Post>();
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                return View(posts);
            }
            else
                return RedirectToAction("Index");

        }

        public IActionResult Edit(long id)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserRole] == UserRole.Writer.ToString())
            {
                Post post = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiUrl);
                    //HTTP GET
                    var responseTask = client.GetAsync("Posts/" + id.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Post>();
                        readTask.Wait();

                        post = readTask.Result;
                    }
                }

                return View(post);
            }
            else
                return RedirectToAction("Edit");
        }

        public IActionResult Submit(long id)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserRole] == UserRole.Writer.ToString())
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiUrl);
                    //HTTP GET
                    var responseTask = client.GetAsync("Posts/Submit/" + id.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
        }

        public IActionResult Approve(long id)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserRole] == UserRole.Editor.ToString())
            {
                string EditorUserId = _httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserId];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiUrl);
                    //HTTP GET
                    var responseTask = client.GetAsync("Posts/Approve/" + id.ToString() + "/" + EditorUserId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
        }

        public IActionResult Reject(long id)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserRole] == UserRole.Editor.ToString())
            {
                string EditorUserId = _httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserId];

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiUrl);
                    //HTTP GET
                    var responseTask = client.GetAsync("Posts/Reject/" + id.ToString() + "/" + EditorUserId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
        }

        public IActionResult Delete(long id)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserRole] == UserRole.Editor.ToString())
            {
                string EditorUserId = _httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserId];

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiUrl);
                    //HTTP GET
                    var responseTask = client.DeleteAsync("Posts/" + id.ToString() + "/" + EditorUserId);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult create(Post post)
        {
            string AuthorUserId = _httpContextAccessor.HttpContext.Request.Cookies[BlogHelper.SessionKeyUserId];

            post.AuhorUserId = Convert.ToInt64(AuthorUserId);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Post>("posts", post);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "An Error has happended, please try again.");

            return View(post);
        }

        [HttpPost]
        public ActionResult Edit(Post post)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Post>("posts/" + post.Id, post);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(post);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
