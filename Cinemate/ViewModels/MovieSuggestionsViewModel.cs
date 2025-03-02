using Cinemate.Models;
using Cinemate.Services;
using Cinemate.Views;
using System.ComponentModel;

namespace Cinemate.ViewModels
{ 
    public class MovieSuggestionsViewModel : INotifyPropertyChanged
    {
        public List<string> GenreOptionsKeys { get; set; }
        public List<string> NumberOptions { get; set; }

        public string NoSuggestions { get; set; }
        public string FavMovies { get; set; }
        public string Genre { get; set; }
        public string NoYearsPublished { get; set; }

        public Command GetSuggestionsCommand { get; set; }

        private string suggestions;
        public string Suggestions { get { return suggestions; } set { suggestions = value; OnPropertyChanged("Suggestions"); } }

        public MovieSuggestionsViewModel()
        {
            GetSuggestionsCommand = new Command(async () => await GetSuggestions());
            NumberOptions = PickerOptions.NumberList;
            GenreOptionsKeys = PickerOptions.GenreDictionary.Keys.ToList();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task GetSuggestions()
        {
            if (NoSuggestions == null || FavMovies == null || Genre == null || NoYearsPublished == null)
            {
                await Shell.Current.DisplayAlert("Error", "Please fill in all the fields!", "OK");
                return;
            }
            Suggestions = await MovieSuggestionsAPI.GetSuggestions(int.Parse(NoSuggestions), FavMovies, Genre, int.Parse(NoYearsPublished));
            await Shell.Current.Navigation.PushAsync(new SuggestedMoviesPage(Suggestions));
        }
    }
}