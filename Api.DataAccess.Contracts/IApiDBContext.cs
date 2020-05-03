using Microsoft.EntityFrameworkCore;
using Api.DataAccess.Contracts.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Api.DataAccess.Contracts
{
    public interface IApiDBContext
    {
        DbSet<PostEntity> Posts { get; set; }
        DbSet<CommentEntity> Comments { get; set; }
        DbSet<UserEntity> Users { get; set; }


        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        void RemoveRange(IEnumerable<object> entities);

        EntityEntry Update(object entity);
    }
}
