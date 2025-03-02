using Cinemate.Views;

namespace Cinemate
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MovieDetailView), typeof(MovieDetailView));
            Routing.RegisterRoute(nameof(AddMovieToMyList), typeof(AddMovieToMyList));
        }
    }
}