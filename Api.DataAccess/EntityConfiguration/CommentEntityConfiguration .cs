using System;
using System.Collections.Generic;
using System.Text;
using Api.DataAccess.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;




namespace Api.DataAccess.EntityConfiguration
{
    public class CommentEntityConfiguration
    {
        public static void SetEntityBuilder(EntityTypeBuilder<CommentEntity> entityBuilder)
        {
            entityBuilder.ToTable("COMMENTS"); 
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).IsRequired();
            
        }
    }
}
