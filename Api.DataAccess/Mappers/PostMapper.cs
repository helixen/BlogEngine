using Api.Business.Models;
using Api.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DataAccess.Mappers
{
    public static class PostMapper
    {
        public static PostEntity Map(Post dto)
        {
            return new PostEntity()
            {
                Id = dto.Id,
                Content = dto.Content,
                ApproverUserId = dto.ApproverUserId,
                AuhorUserId = dto.AuhorUserId,
                CreatedDate = dto.CreatedDate,
                PublishedDate = dto.PublishedDate,
                RejectedDate = dto.RejectedDate,
                DeletedDate = dto.DeletedDate,
                Status = Map(dto.Status),
                SubmittedDate = dto.SubmittedDate,
                AuthorName = dto.AuthorName,
                EditorName=dto.EditorName,
                Comments = CommentMapper.Map(dto.Comments)
            };
        }

        public static Post Map(PostEntity entity)
        {
            return new Post()
            {
                Id = entity.Id,
                Content = entity.Content,
                ApproverUserId = entity.ApproverUserId,
                AuhorUserId = entity.AuhorUserId,
                CreatedDate = entity.CreatedDate,
                PublishedDate = entity.PublishedDate,
                RejectedDate = entity.RejectedDate,
                DeletedDate = entity.DeletedDate,
                Status = Map(entity.Status),
                SubmittedDate = entity.SubmittedDate,
                AuthorName = entity.AuthorName,
                EditorName = entity.EditorName,
                Comments = CommentMapper.Map(entity.Comments)

            };
        }

        public static IEnumerable<PostEntity> Map(IEnumerable<Post> dList)
        {
            List<PostEntity> mappedList = new List<PostEntity>();
            foreach(Post vTo in dList)
            {
                mappedList.Add(Map(vTo));
            }

            return mappedList;
        }

        public static IEnumerable<Post> Map(IEnumerable<PostEntity> dList)
        {
            List<Post> mappedList = new List<Post>();
            foreach (PostEntity vTo in dList)
            {
                mappedList.Add(Map(vTo));
            }

            return mappedList;
        }

        public static Contracts.Entities.PostStatus Map(Business.Models.PostStatus status)
        {
             switch (status)
            {
                case Business.Models.PostStatus.Created: return Contracts.Entities.PostStatus.Created;
                case Business.Models.PostStatus.Submitted: return Contracts.Entities.PostStatus.Submitted;
                case Business.Models.PostStatus.Approved: return Contracts.Entities.PostStatus.Approved;
                case Business.Models.PostStatus.Rejected: return Contracts.Entities.PostStatus.Rejected;
                case Business.Models.PostStatus.Deleted: return Contracts.Entities.PostStatus.Deleted;
                default: return Contracts.Entities.PostStatus.Unknown;

            }
        }

        public static Business.Models.PostStatus Map(Contracts.Entities.PostStatus status)
        {
            switch (status)
            {
                case Contracts.Entities.PostStatus.Created: return Business.Models.PostStatus.Created;
                case Contracts.Entities.PostStatus.Submitted: return Business.Models.PostStatus.Submitted;
                case Contracts.Entities.PostStatus.Approved: return Business.Models.PostStatus.Approved;
                case Contracts.Entities.PostStatus.Rejected: return Business.Models.PostStatus.Rejected;
                case Contracts.Entities.PostStatus.Deleted: return Business.Models.PostStatus.Deleted;
                default: return Business.Models.PostStatus.Unknown;

            }
        }
    }
}
