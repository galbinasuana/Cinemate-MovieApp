using Cinemate.ViewModels;
namespace Cinemate.Views;

public partial class AddMovieToMyList : ContentPage
{
    public AddMovieToMyList(AddMovieToMyListViewModel addMovieToMyListViewModel)
    {
        InitializeComponent();
        BindingContext = addMovieToMyListViewModel;
    }
}