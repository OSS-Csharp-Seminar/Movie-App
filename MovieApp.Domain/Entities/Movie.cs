using System;
using System.Collections.Generic;

namespace MovieApp.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }

        // Navigation properties
        public ICollection<Review> Reviews { get; set; }
        public ICollection<WatchlistItem> WatchlistItems { get; set; }

        public Movie()
        {
            Reviews = new List<Review>();
            WatchlistItems = new List<WatchlistItem>();
        }
    }
}
