using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Comment
    {
        public long Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public long PostId { get; set; }
        public long? AuthorUserId { get; set; }
        public String AuthorName { get; set; }
    }
}
