using Cinemate.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace Cinemate.ViewModels
{
    [QueryProperty(nameof(MovieLibrary), "MovieLibrary")]
    public partial class MovieDetailViewModel : ObservableObject, INotifyPropertyChanged
    {
        private MovieLibrary _movieLibrary;

        [ObservableProperty]
        public MovieLibrary movieLibrary;

        private DaoMovie daoMovie = DaoMovie.GetDaoMovie();

        [RelayCommand]
        public async Task DeleteMovie()
        {
            MovieLibrary movieToBeDeleted = await daoMovie.FindMovieById(movieLibrary.Id);

            bool deleteConfirmed = await Shell.Current.DisplayAlert(
                "Delete Movie",
                "Are you sure you want to delete this movie from your journal?",
                "Yes",
                "No"
            );

            if(deleteConfirmed)
            {
                if(movieToBeDeleted != null)
                {
                    Console.WriteLine("Movie found, attempting to delete...");

                    int result = await daoMovie.DeleteMovie(movieLibrary);
                    if (result > 0)
                        Console.WriteLine("Movie deleted successfully.");
                    else
                        Console.WriteLine("Failed to delete movie.");

                    var remainingMovies = await daoMovie.GetMovies();
                    foreach (var remainingMovie in remainingMovies)
                    {
                        Console.WriteLine($"Remaining Movie: {remainingMovie.Title}");
                    }

                    await Shell.Current.GoToAsync("..");
                } 
                else
                    Console.WriteLine("Movie not found in the database.");
                await Shell.Current.GoToAsync("..");
            }


            //if (deleteConfirmed && movieLibrary != null)
            //{
            //    DaoMovie daoMovie = DaoMovie.GetDaoMovie();
            //    int result = await daoMovie.DeleteMovie(movieLibrary);
            //    if (result > 0)
            //        Console.WriteLine("Movie deleted successfully.");
            //    else
            //        Console.WriteLine("Failed to delete movie.");


            //    var remainingMovies = await daoMovie.GetMovies();
            //    foreach (var remainingMovie in remainingMovies)
            //    {
            //        Console.WriteLine($"Remaining Movie: {remainingMovie.Title}");
            //    }

            //    await Shell.Current.GoToAsync("..");
            //}
        }


        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
