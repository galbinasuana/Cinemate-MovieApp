using SQLite;

namespace Cinemate.Models
{
    public class DaoMovie
    {
        static DaoMovie daoMovie;
        SQLiteAsyncConnection connAsync;

        private DaoMovie()
        {
            connAsync = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "movie_list.db"));
            connAsync.CreateTableAsync<MovieLibrary>().Wait();
        }

        public static DaoMovie GetDaoMovie()
        {
            if (daoMovie == null)
            {
                daoMovie = new DaoMovie();
            }

            return daoMovie;

        }
        public async Task<MovieLibrary> FindMovieById(int id)
        {
            return await connAsync.FindAsync<MovieLibrary>(id);
        }

        public async Task<int> AddMovie(MovieLibrary movie)
        {
            try
            {
                var result = await connAsync.InsertAsync(movie);
                if(result != 0)
                    Console.WriteLine($"Movie '{movie.Title}' added successfully with ID: {movie.Id}");
                else
                    Console.WriteLine("Failed to add the movie.");

                return movie.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding movie to database: {ex.Message}");
                return 0;
            }
        }

        public async Task<int> DeleteMovie(MovieLibrary movie)
        {
            try
            {
                Console.WriteLine($"Trying to delete movie with ID: {movie.Id} and Title: {movie.Title}");
                return await connAsync.DeleteAsync(movie);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding movie to database: {ex.Message}");
                return 0;
            }
        }

        public async Task<int> DeleteAllMovie()
        {
            try
            {
                return await connAsync.DeleteAllAsync<MovieLibrary>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding movie to database: {ex.Message}");
                return 0;
            }
        }


        public Task<List<MovieLibrary>> GetMovies()
        {
            return connAsync.QueryAsync<MovieLibrary>("SELECT * FROM movie_list");
        }

    }

}
