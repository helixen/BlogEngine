using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.DataAccess.Contracts.Entities
{
    public class CommentEntity
    {
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("content")]
        public string Content { get; set; }
        [Required]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Column("author_user_id")]
        public long? AuthorUserId { get; set; }
        [Required]
        [Column("post_id")]
        public long PostId { get; set; }
        [NotMapped]
        public String AuthorName { get; set; }
        public PostEntity post { get; set; }

    }
}
