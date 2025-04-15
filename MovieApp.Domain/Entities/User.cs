using System;
using System.Collections.Generic;

namespace MovieApp.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? LastLogin { get; set; }

        // Navigation properties
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<WatchlistItem> WatchlistItems { get; set; }

        public User()
        {
            Reviews = new List<Review>();
            Comments = new List<Comment>();
            WatchlistItems = new List<WatchlistItem>();
        }
    }
}
