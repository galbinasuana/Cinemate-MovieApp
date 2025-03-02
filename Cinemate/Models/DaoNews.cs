using SQLite;

namespace Cinemate.Models
{
    public class DaoNews
    {
        SQLiteAsyncConnection connAsync;

        public DaoNews()
        {
            connAsync = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "news_article.db"));
            connAsync.CreateTableAsync<NewsArticle>().Wait();
        }

        public async Task<int> AddArticle(NewsArticle newsArticle)
        {
            return await connAsync.InsertAsync(newsArticle);
        }

        public async Task<int> AddListArticles(List<NewsArticle> listArticles)
        {
            return await connAsync.InsertAllAsync(listArticles);
        }

        public async Task<int> UpdateArticle(NewsArticle newsArticle)
        {
            return await connAsync.UpdateAsync(newsArticle);
        }

        public Task<List<NewsArticle>> GetArticles()
        {
            return connAsync.QueryAsync<NewsArticle>("SELECT * FROM news_article WHERE date = ?", DateTime.Now.ToString("yyyy-MM-dd"));
        }

        public async Task<int> DeleteAllArticles()
        {
            return await connAsync.DeleteAllAsync<NewsArticle>();
        }
    }
}
