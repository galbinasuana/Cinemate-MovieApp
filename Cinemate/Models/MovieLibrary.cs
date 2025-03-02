using SQLite;
using Microsoft.Maui.Controls;

namespace Cinemate.Models
{
    [Table("movie_list")]
    public class MovieLibrary
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }
        [Column("cover")]
        public string Cover { get; set; }
        [Column("rating")]
        public double Rating { get; set; }
        [Column("reviews")]
        public double Reviews { get; set; }
        [Column("metascore")]
        public double Metascore { get; set; }
        [Column("criticReviews")]
        public double CriticReviews { get; set; }
        [Column("start_date")]
        public string StartDate { get; set; }
        [Column("status")]
        public string Status { get; set; }
        [Column("summary")]
        public string Summary { get; set; }
        [Column("my_review")]
        public string MyReview { get; set; }
        [Column("categories")]
        public string CategoriesSerialized { get; set; }

        [Ignore]
        public IEnumerable<string> Categories
        {
            get => CategoriesSerialized?.Split(',') ?? new string[0];
            set => CategoriesSerialized = string.Join(",", value);
        }

        [Ignore] 
        public ImageSource CoverImage
        {
            get => string.IsNullOrEmpty(Cover) ? null : ImageConverter.Base64ToImage(Cover);
        }


        public MovieLibrary() { }
    }
}