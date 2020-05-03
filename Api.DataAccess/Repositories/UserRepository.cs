using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.DataAccess.Contracts;
using Api.DataAccess.Contracts.Entities;
using Api.DataAccess.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly IApiDBContext _apiDBContext;

        public UserRepository(IApiDBContext apiDBContext)
        {
            _apiDBContext = apiDBContext;
        }

        public async Task<UserEntity> Add(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> Update(long id, UserEntity UserEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> Get(long idEntity)
        {
            var result = await _apiDBContext.Users.FirstOrDefaultAsync(x => x.Id == idEntity);
            if(result == null)
                throw new Exception("Object not found");

            return result;
        }

        public async Task<UserEntity> Get(string userName)
        {
            var result = await _apiDBContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (result == null)
                throw new Exception("Object not found");

            return result;
        }

        public async Task DeleteAsync(long idEntity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exist(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> Update(int id, UserEntity element)
        {
            throw new NotImplementedException();
        }
    }
}
