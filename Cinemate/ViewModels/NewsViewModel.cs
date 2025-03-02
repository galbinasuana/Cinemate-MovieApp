using Cinemate.Models;
using Cinemate.Views;
using System.ComponentModel;

namespace Cinemate.ViewModels
{
    public class NewsViewModel : INotifyPropertyChanged
    {
        private DaoNews daoNews;
        public Command GetArticlesCommand { get; set; }
        public Command SetArticlesCommand { get; set; }

        private List<NewsArticle> articlesList;
        public List<NewsArticle> ArticlesList { get { return articlesList; } set { articlesList = value; OnPropertyChanged("ArticlesList"); } }


        public NewsViewModel()
        {
            daoNews = new DaoNews();
            GetArticlesCommand = new Command(async () => await GetArticles());
            SetArticlesCommand = new Command(async (article) => await SetArticles(article));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task GetArticles()
        {
            try
            {
                ArticlesList = await daoNews.GetArticles();
                if (ArticlesList == null || !ArticlesList.Any())
                {
                    ArticlesList = await NewsWebScraping.GetArticles();
                    if (ArticlesList != null && ArticlesList.Any())
                    {
                        await daoNews.DeleteAllArticles();
                        await daoNews.AddListArticles(ArticlesList);
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "The page could not be loaded properly. Please check your internet connection and try again.", "OK");
                        await Shell.Current.Navigation.PopAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                await Shell.Current.Navigation.PopAsync();
            }
        }

        private async Task SetArticles(object articleParam)
        {
            if (articleParam is NewsArticle article)
            {
                if (article.Content == null)
                {
                    await NewsWebScraping.SetArticleContent(article);
                    await daoNews.UpdateArticle(article);
                }
                await Shell.Current.Navigation.PushAsync(new ArticleContentPage(article));
            }
        }
    }
}
