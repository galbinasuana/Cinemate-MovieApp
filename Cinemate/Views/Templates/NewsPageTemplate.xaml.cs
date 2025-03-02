using Cinemate.ViewModels;
namespace Cinemate.Views.Templates;

public partial class NewsPageTemplate : ContentPage
{
    NewsViewModel newsViewModel = new NewsViewModel();

    public NewsPageTemplate()
    {
        InitializeComponent();
        BindingContext = newsViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        newsViewModel.GetArticlesCommand.Execute(null);
    } 
}