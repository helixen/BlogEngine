using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.DataAccess.Contracts.Entities
{
    public class UserEntity
    {
        [Required]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("username")]
        public string UserName { get; set; }
        [Required]
        [Column("password")]
        public string Password { get; set; }
        [Required]
        [Column("role")]
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Writer = 0,
        Editor = 1,
        Unknown
    }
}
