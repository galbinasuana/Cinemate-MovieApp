using Cinemate.ViewModels;

namespace Cinemate.Views;

public partial class MovieDetailView : ContentPage
{
    public MovieDetailView(MovieDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}