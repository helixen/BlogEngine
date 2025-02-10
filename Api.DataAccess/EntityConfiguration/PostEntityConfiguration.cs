using System;
using System.Collections.Generic;
using System.Text;
using Api.DataAccess.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;




namespace Api.DataAccess.EntityConfiguration
{
    public class PostEntityConfiguration
    {
        public static void SetEntityBuilder(EntityTypeBuilder<PostEntity> entityBuilder)
        {
            entityBuilder.ToTable("POSTS"); 
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).IsRequired();
            
        }
    }
}
