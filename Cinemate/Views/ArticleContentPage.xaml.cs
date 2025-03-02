using Cinemate.Models;

namespace Cinemate.Views;

public partial class ArticleContentPage : ContentPage
{
    public ArticleContentPage(NewsArticle article)
    {
        InitializeComponent();
        BindingContext = article;
    }
}