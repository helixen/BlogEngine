using System;

using System.Threading.Tasks;
using Api.Business.Models;
using System.Collections.Generic;

namespace Api.Application.Contracts.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(User user);
        Task<User> Get(long id);
    }
}
