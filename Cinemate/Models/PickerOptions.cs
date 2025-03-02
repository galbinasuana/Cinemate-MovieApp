namespace Cinemate.Models
{
    public static class PickerOptions
    {
        public static List<string> MovieStatuses => new List<string>
        {
            "Watching", "Completed", "On Hold", "Dropped", "Plan to Watch"
        };

        public static List<string> NumberList => new List<string>
        {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"
        };

        public static Dictionary<string, int> GenreDictionary => new Dictionary<string, int>
        {
            { "Action", 28 },
            { "Adventure", 12 },
            { "Animation", 16 },
            { "Comedy", 35 },
            { "Crime", 80 },
            { "Documentary", 99 },
            { "Drama", 18 },
            { "Family", 10751 },
            { "Fantasy", 14 },
            { "History", 36 },
            { "Horror", 27 },
            { "Music", 10402 },
            { "Mystery", 9648 },
            { "Romance", 10749 },
            { "Science Fiction", 878 },
            { "TV Movie", 10770 },
            { "Thriller", 53 },
            { "War", 10752 },
            { "Western", 37 }
        };

        public static List<string> YearsList
        {
            get
            {
                var years = new List<string>();
                for (int year = DateTime.Now.Year; year >= 2014; year--)
                {
                    years.Add(year.ToString());
                }
                return years;
            }
        }

        public static Dictionary<string, string> SortByDictionary => new Dictionary<string, string>
        {
            {"Popularity", "popularity.desc" },
            { "Release date (ascending)", "primary_release_date.asc" },
            {"Release date (descending)", "primary_release_date.desc" },
            {"Number of votes", "vote_count.desc" }
        };
    }
}
