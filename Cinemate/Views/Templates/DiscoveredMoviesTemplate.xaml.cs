using Cinemate.Models;

namespace Cinemate.Views.Templates;

public partial class DiscoveredMoviesTemplate : ContentPage
{
    public DiscoveredMoviesTemplate(List<Movie> movieList)
    {
        InitializeComponent();
        listMovies.ItemsSource = movieList;
    }
}