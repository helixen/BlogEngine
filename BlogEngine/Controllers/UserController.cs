using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Models;
using System.Net.Http;
using Helpers;
using Microsoft.Extensions.Configuration;

namespace BlogEngine.Controllers
{

    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        string _apiUrl;

        public UserController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _apiUrl = _configuration["ApiUrl"];

        }

        public IActionResult SignOut()
        {
            Response.Cookies.Delete(BlogHelper.SessionKeyUserName);
            Response.Cookies.Delete(BlogHelper.SessionKeyUserId);
            Response.Cookies.Delete(BlogHelper.SessionKeyUserRole);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Authenticate(User user)
        {
            User verifiedUser = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);
                //HTTP GET
                var responseTask = client.GetAsync("User/" + user.UserName + "/" + user.Password);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<User>();
                    readTask.Wait();

                    verifiedUser = readTask.Result;

                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Append(BlogHelper.SessionKeyUserName, verifiedUser.UserName, option);
                    Response.Cookies.Append(BlogHelper.SessionKeyUserId, verifiedUser.Id.ToString(), option);
                    Response.Cookies.Append(BlogHelper.SessionKeyUserRole, verifiedUser.Role.ToString(), option);
                }
                
            }
            
            return RedirectToAction("Index", "Home");
        }
    }
}