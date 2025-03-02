using System.Net;
using HtmlAgilityPack;

namespace Cinemate.Models
{
    class NewsWebScraping
    {
        public static async Task<List<NewsArticle>> GetArticles()
        {
            HttpClient client = new HttpClient();
            string html = await client.GetStringAsync("https://m.imdb.com/news/top/");
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            HtmlNodeCollection articlesHtml = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'ipc-list-card--base')]");
            if (articlesHtml != null)
            {
                List<NewsArticle> articleList = new List<NewsArticle>();

                foreach (HtmlNode node in articlesHtml)
                {
                    HtmlNode titleNode = node.Descendants("a").FirstOrDefault(a => a.Attributes["href"] != null && !string.IsNullOrWhiteSpace(a.InnerText));
                    string title = WebUtility.HtmlDecode(titleNode?.InnerText.Trim());
                    string articleUrl = titleNode?.GetAttributeValue("href", string.Empty);
                    HtmlNode imgNode = node.Descendants("img").FirstOrDefault();
                    string imageUrl = imgNode?.GetAttributeValue("src", string.Empty);

                    if (!string.IsNullOrEmpty(imageUrl) && !string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(articleUrl))
                    {
                        articleList.Add(new NewsArticle(title, imageUrl, articleUrl));
                    }
                }
                return articleList;
            }
            return null;
        }

        public static async Task SetArticleContent(NewsArticle newsArticle)
        {
            HttpClient client = new HttpClient();
            string html = await client.GetStringAsync(newsArticle.ArticleUrl);
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            HtmlNodeCollection paragraphs = htmlDocument.DocumentNode.SelectNodes("//p[contains(@class, 'paragraph')]");
            if (paragraphs != null)
            {
                string content = "";
                foreach (var paragraph in paragraphs)
                {
                    content += WebUtility.HtmlDecode(paragraph.InnerText.Trim());
                }
                newsArticle.Content = content;
            }

            /*HtmlNode dateNode = htmlDocument.DocumentNode.SelectSingleNode("//time[contains(@class, 'c-timestamp')]");
            if (dateNode != null)
            {
                string dateText = dateNode.InnerText.Trim();
                newsArticle.Date = WebUtility.HtmlDecode(dateText);
            }*/
        }
    }
}
