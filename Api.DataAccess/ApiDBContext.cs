using System;
using Microsoft.EntityFrameworkCore;
using Api.DataAccess.Contracts;
using Api.DataAccess.Contracts.Entities;
using Api.DataAccess.EntityConfiguration;



namespace Api.DataAccess
{
    public class ApiDBContext: DbContext, IApiDBContext
    {
        public DbSet<PostEntity> Posts{ get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<UserEntity> Users { get; set; }


        public ApiDBContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PostEntityConfiguration.SetEntityBuilder(modelBuilder.Entity<PostEntity>());
            CommentEntityConfiguration.SetEntityBuilder(modelBuilder.Entity<CommentEntity>());
            UserEntityConfiguration.SetEntityBuilder(modelBuilder.Entity<UserEntity>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
