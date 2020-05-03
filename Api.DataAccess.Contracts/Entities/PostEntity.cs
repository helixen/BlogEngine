using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.DataAccess.Contracts.Entities
{
    public class PostEntity
    {
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("content")]
        public string Content { get; set; }
        [Column("status")]
        [Required]
        public PostStatus Status { get; set; }
        [Required]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("published_date")]
        public Nullable<DateTime> PublishedDate { get; set; }
        [Column("submitted_date")]
        public Nullable<DateTime> SubmittedDate { get; set; }
        [Column("rejected_date")]
        public Nullable<DateTime> RejectedDate { get; set; }
        [Column("deleted_date")] 
        public Nullable<DateTime> DeletedDate { get; set; }

        [Required]
        [Column("author_user_id")]
        public long AuhorUserId { get; set; }
        [Column("appover_user_id")]
        public long? ApproverUserId { get; set; }
        [NotMapped]
        public String AuthorName { get; set; }
        [NotMapped]
        public String EditorName { get; set; }
        public IEnumerable<CommentEntity> Comments { get; set; }
    }

    public enum PostStatus
    {
        Created = 0,
        Submitted = 1,
        Approved = 2,
        Rejected = 3,
        Deleted = 4,
        Unknown

    }
}
