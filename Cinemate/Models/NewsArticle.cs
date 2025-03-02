using SQLite;

namespace Cinemate.Models
{
    [Table("news_article")]
    public class NewsArticle
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; }

        [Column("article_url")]
        public string ArticleUrl { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("date")]
        public string Date { get; set; }

        public NewsArticle() { }
        public NewsArticle(string title, string imageUrl, string articleUrl)
        {
            this.Title = title;
            this.ImageUrl = imageUrl;
            this.ArticleUrl = articleUrl;
            this.Date = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
