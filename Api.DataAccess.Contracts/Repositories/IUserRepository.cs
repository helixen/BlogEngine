using System.Collections.Generic;
using Api.DataAccess.Contracts.Entities;
using System.Threading.Tasks;


namespace Api.DataAccess.Contracts.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserEntity> Get(string userName);
    }
}
