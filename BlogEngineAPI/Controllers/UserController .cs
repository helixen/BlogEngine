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
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService UserService)
        {
            _service = UserService;
        }

        [HttpGet("{username}/{password}")]
        public async Task<ActionResult<User>> Get(string username, string password)
        {
            var user = await _service.Authenticate(new User { UserName = username, Password = password });
            return Ok(user);
        }
    }
}
