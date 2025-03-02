using Cinemate.ViewModels;

namespace Cinemate.Views;

public partial class MoviesView : ContentPage
{
	public MoviesView(MoviesViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as MoviesViewModel;
        if (viewModel != null)
        {
            viewModel.LoadData();
        }
    }
}