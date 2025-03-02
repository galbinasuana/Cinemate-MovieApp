using Cinemate.ViewModels;

namespace Cinemate.Views;

public partial class MovieSuggestionsPage : ContentPage
{
    MovieSuggestionsViewModel movieSuggestionsViewModel = new MovieSuggestionsViewModel();

    public MovieSuggestionsPage()
    {
        InitializeComponent();
        BindingContext = movieSuggestionsViewModel;
    }
}