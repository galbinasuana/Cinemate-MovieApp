using Cinemate.ViewModels;

namespace Cinemate.Views;

public partial class DiscoverMoviesPage : ContentPage
{
    DiscoverMoviesViewModel discoverMoviesViewModel = new DiscoverMoviesViewModel();

    public DiscoverMoviesPage()
    {
        InitializeComponent();
        BindingContext = discoverMoviesViewModel;
    }
}