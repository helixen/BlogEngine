using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Contracts.Services;
using Api.Business.Models;
using Api.DataAccess.Contracts.Repositories;
using Api.DataAccess.Mappers;
using System;

namespace Api.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        //Si deseo un objeto diferente al mio propio debo de llamar el servicio del mismo, no debo de usar el repositorio de otros objetos

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Authenticate(User user)
        {
            var result = await _userRepository.Get(user.UserName);
            if (user.Password == result.Password)
                return UserMapper.Map(result);
            else
                throw new Exception("Authentication credentials not valid");
        }

        public async Task<User> Get(long id)
        {
            var result = await _userRepository.Get(id);            
            return UserMapper.Map(result);
        }
    }
}
