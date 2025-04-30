using System;

namespace MovieApp.Domain.Entities
{
    public class WatchlistItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime AddedAt { get; set; }

        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
