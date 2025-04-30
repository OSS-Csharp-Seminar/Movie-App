using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MovieApp.Domain.Entities
{
    public class User : IdentityUser
    {
        public DateTime RegisterDate { get; set; }
        public DateTime? LastLogin { get; set; }

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
