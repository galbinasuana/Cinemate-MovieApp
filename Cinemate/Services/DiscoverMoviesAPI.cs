using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Cinemate.Models
{
    class DiscoverMoviesAPI
    {
        private static string getURI(string releaseYear, string sortBy, string genreID)
        {
            string uri = "https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=en-US&page=1";
            if (releaseYear != null) { uri += "&primary_release_year=" + releaseYear; }
            uri += "&sort_by=" + sortBy;
            if (genreID != null) { uri += "&with_genres=" + genreID; }
            return uri;
        }

        public static async Task<List<Movie>> getMovies(Dictionary<string, int> genreOptions, string releaseYear, string sortBy, string genreID)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(getURI(releaseYear, sortBy, genreID)),
                    Headers =
                    {
                        {"accept", "application/json"},
                        {"Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI5NDgzMmIyNmM5NTZkNDgyM2Q3NTY5OTBjMDcyNjFlMSIsInN1YiI6IjY1ZGQ4NmRiYzQzM2VhMDE2MzNjMmQwNCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.6BVWyOI4744LaPF53-IlpW_2wx_t6BYRqah35Aujaek"}
                    }
                };
                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Could not retrieve movies.");
                        return null;
                    }

                    string body = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(body);

                    List<Movie> movieList = new List<Movie>();
                    foreach (var item in jsonResponse["results"])
                    {
                        string title = (string)item["title"];
                        string overview = (string)item["overview"];
                        List<int> genreIDs = item["genre_ids"].Select(id => (int)id).ToList();
                        string genres = string.Join(", ", genreIDs.Select(id => genreOptions.FirstOrDefault(g => g.Value == id).Key));
                        DateTime releaseDate = DateTime.ParseExact((string)item["release_date"], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        string posterPath = "https://image.tmdb.org/t/p/w500" + (string)item["poster_path"];
                        double popularity = (double)item["popularity"];
                        double avgRating = (double)item["vote_average"];
                        int noVotes = (int)item["vote_count"];

                        movieList.Add(new Movie(title, overview, genres, releaseDate, posterPath, popularity, avgRating, noVotes));
                    }
                    return movieList;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return null;
            }
        }
    }
}