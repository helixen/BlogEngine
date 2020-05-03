using Api.Business.Models;
using Api.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DataAccess.Mappers
{
    public static class CommentMapper
    {
        public static CommentEntity Map(Comment dto)
        {
            return new CommentEntity()
            {
                Id = dto.Id,
                Content = dto.Content,
                AuthorUserId = dto.AuthorUserId,
                CreatedDate = dto.CreatedDate,
                PostId = dto.PostId,
                AuthorName = dto.AuthorName
            };
        }

        public static Comment Map(CommentEntity entity)
        {
            return new Comment()
            {
                Id = entity.Id,
                Content = entity.Content,
                AuthorUserId = entity.AuthorUserId,
                CreatedDate = entity.CreatedDate,
                PostId = entity.PostId,
                AuthorName = entity.AuthorName
            };
        }

        public static IEnumerable<CommentEntity> Map(IEnumerable<Comment> dList)
        {
            List<CommentEntity> mappedList = new List<CommentEntity>();
            if (dList != null)
            {
                foreach (Comment vTo in dList)
                {
                    mappedList.Add(Map(vTo));
                }
            }

            return mappedList;
        }

        public static IEnumerable<Comment> Map(IEnumerable<CommentEntity> dList)
        {
            List<Comment> mappedList = new List<Comment>();
            if (dList != null)
            {
                foreach (CommentEntity vTo in dList)
                {
                    mappedList.Add(Map(vTo));
                }
            }

            return mappedList;
        }
    }
}
