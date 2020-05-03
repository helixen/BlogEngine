using Api.Business.Models;
using Api.DataAccess.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.DataAccess.Mappers
{
    public static class UserMapper
    {
        public static UserEntity Map(User dto)
        {
            return new UserEntity()
            {
                Id = dto.Id,
                UserName = dto.UserName,
                Password = dto.Password,
                Role  = Map(dto.Role),
            };
        }

        public static User Map(UserEntity entity)
        {
            return new User()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Password = entity.Password,
                Role = Map(entity.Role),
            };
        }

        public static IEnumerable<UserEntity> Map(IEnumerable<User> dList)
        {
            List<UserEntity> mappedList = new List<UserEntity>();
            foreach(User vTo in dList)
            {
                mappedList.Add(Map(vTo));
            }

            return mappedList;
        }

        public static IEnumerable<User> Map(IEnumerable<UserEntity> dList)
        {
            List<User> mappedList = new List<User>();
            foreach (UserEntity vTo in dList)
            {
                mappedList.Add(Map(vTo));
            }

            return mappedList;
        }

        public static Contracts.Entities.UserRole Map(Business.Models.UserRole status)
        {
             switch (status)
            {
                case Business.Models.UserRole.Editor: return Contracts.Entities.UserRole.Editor;
                case Business.Models.UserRole.Writer: return Contracts.Entities.UserRole.Writer;
                default: return Contracts.Entities.UserRole.Unknown;

            }
        }

        public static Business.Models.UserRole Map(Contracts.Entities.UserRole status)
        {
            switch (status)
            {
                case Contracts.Entities.UserRole.Editor: return Business.Models.UserRole.Editor;
                case Contracts.Entities.UserRole.Writer: return Business.Models.UserRole.Writer;
                default: return Business.Models.UserRole.Unknown;

            }
        }
    }
}
