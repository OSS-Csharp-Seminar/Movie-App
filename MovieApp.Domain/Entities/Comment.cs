using System;

namespace MovieApp.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ReviewId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ParentId { get; set; }

        public User User { get; set; }
        public Review Review { get; set; }
        public Comment Parent { get; set; }
    }
}
