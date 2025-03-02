using Cinemate.Models;
using Cinemate.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Cinemate.ViewModels
{
    public partial class MoviesViewModel : ObservableObject
    {
        private readonly MoviesCollection moviesCollection;
        private readonly DaoMovie daoMovie;

        [ObservableProperty]
        public int carouselPosition;

        [ObservableProperty]
        private int entriesCount;

        [ObservableProperty]
        private string selectedSource;

        [ObservableProperty]
        private MovieLibrary selectedMovie;

        [ObservableProperty]
        private FilterOption selectedFilterOption;

        public List<MovieLibrary> listTest;
        public ObservableCollection<MovieLibrary> FilteredMovies { get; set; } = new ObservableCollection<MovieLibrary>();

        public ObservableCollection<string> Sources { get; set; } = new ObservableCollection<string>();
        public List<MovieLibrary> Movies { get; set; }
        public ObservableCollection<FilterOption> FilterOptions { get; } = new ObservableCollection<FilterOption>();



        public MoviesViewModel(MoviesCollection moviesCollection)
        {
            this.moviesCollection = moviesCollection;
            daoMovie = DaoMovie.GetDaoMovie();

            //addMoviesToDB();
            LoadData();
        }

        async void addMoviesToDB()
        {
            foreach (var movie in moviesCollection.GetMovies())
            {
                daoMovie.AddMovie(movie);
            }
        }

        public async void LoadData()
        {
            Movies = await daoMovie.GetMovies();

            foreach(var source in moviesCollection.GetMovieSources())
            {
                Sources.Add(source);
            }
            foreach (string option in moviesCollection.GetFilterOptions())
            {
                FilterOptions.Add(new FilterOption { Name = option, IsSelected = false });
            }

            SelectedSource = "All";
            SelectedFilterOption = FilterOptions.FirstOrDefault();
            ApplyFilters();
        }

        [RelayCommand]
        void SelectFilterOption(FilterOption option)
        {
            if (SelectedFilterOption != null)
            {
                SelectedFilterOption.IsSelected = false;
            }

            SelectedFilterOption = option;

            if (SelectedFilterOption != null)
            {
                SelectedFilterOption.IsSelected = true;
            }

            ApplyFilters();
        }

        private async Task ResetCarouselPosition()
        {
            await Task.Delay(500);
            CarouselPosition = 0;
        }

        [RelayCommand]
        void ApplyFilters()
        {
            var currentDate = DateTime.Today;
            if (SelectedSource == "All")
            {
                listTest = Movies;
            }
            else if (!string.IsNullOrEmpty(SelectedSource))
            {
                listTest = Movies.Where(m => m.Status == SelectedSource).ToList();
            }

            if (SelectedFilterOption != null)
            {
                switch (SelectedFilterOption.Name)
                {
                    case "A - Z":
                        listTest = listTest.OrderBy(m => m.Title).ToList();
                        break;
                    case "Z - A":
                        listTest = listTest.OrderByDescending(m => m.Title).ToList();
                        break;
                    case "Score":
                        listTest = listTest.OrderByDescending(m => m.Rating).ToList();
                        break;
                    case "Upcoming":
                        listTest = listTest
                            .Where(m => DateTime.ParseExact(m.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture) >= currentDate)
                            .OrderBy(m => m.StartDate).ToList();
                        break;
                    case "Past":
                        listTest = listTest
                            .Where(m => DateTime.ParseExact(m.StartDate, "dd-MM-yyyy", CultureInfo.InvariantCulture) < currentDate)
                            .OrderByDescending(m => m.StartDate).ToList();
                        break;
                }
            }
            FilteredMovies.Clear();
            foreach(MovieLibrary movie in listTest)
            {
                FilteredMovies.Add(movie);
            }
            EntriesCount = FilteredMovies.Count;
            ResetCarouselPosition();
        }

        partial void OnSelectedSourceChanged(string value)
        {
            ApplyFilters();
        }

        partial void OnSelectedFilterOptionChanged(FilterOption value)
        {
            ApplyFilters();
        }


        [RelayCommand]
        async Task GoToDetails(MovieLibrary movie)
        {
            if (movie == null)
                return;

            await Shell.Current.GoToAsync(nameof(MovieDetailView), true, new Dictionary<string, object>
            {
                {"MovieLibrary", movie }
            });
        }

        [RelayCommand]
        async Task NavigateToAddMoviePage()
        {
            await Shell.Current.GoToAsync(nameof(AddMovieToMyList));
        }


    }
}
