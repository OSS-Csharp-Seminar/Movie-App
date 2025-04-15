using System;
using System.Collections.Generic;

namespace MovieApp.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Movie Movie { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Review()
        {
            Comments = new List<Comment>();
        }
    }
}
