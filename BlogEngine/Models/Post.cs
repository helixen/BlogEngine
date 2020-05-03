using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Post
    {
        public long Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public PostStatus Status { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> PublishedDate { get; set; }
        public Nullable<DateTime> SubmittedDate { get; set; }
        public Nullable<DateTime> RejectedDate { get; set; }
        public Nullable<DateTime> DeletedDate { get; set; }
        [Required]
        public long AuhorUserId { get; set; }
        public long? ApproverUserId { get; set; }
        public String AuthorName { get; set; }
        public String EditorName { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
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
