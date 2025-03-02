namespace Cinemate.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Genres { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PosterPath { get; set; }
        public double Popularity { get; set; }
        public double AvgRating { get; set; }
        public int NoVotes { get; set; }

        public Movie(string title, string overview, string genres, DateTime releaseDate, string posterPath, double popularity, double avgRating, int noVotes)
        {
            this.Title = title;
            this.Overview = overview;
            this.Genres = genres;
            this.ReleaseDate = releaseDate;
            this.PosterPath = posterPath;
            this.Popularity = popularity;
            this.AvgRating = avgRating;
            this.NoVotes = noVotes;
        }
    }
}