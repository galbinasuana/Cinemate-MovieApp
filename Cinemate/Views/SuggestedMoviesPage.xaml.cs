namespace Cinemate.Views;

public partial class SuggestedMoviesPage : ContentPage
{
    public string SuggestedMovies { get; set; }

    public SuggestedMoviesPage(string suggestedMovies)
    {
        InitializeComponent();
        SuggestedMovies = suggestedMovies;
        this.BindingContext = this;
        SuggestionLabel.SizeChanged += OnContentSizeChanged;
    }

    private void OnContentSizeChanged(object sender, EventArgs e)
    {
        var label = sender as Label; 
        if (label != null)
        {
            var requiredHeight = label.Height;
            SuggestionFrame.HeightRequest = Math.Min(requiredHeight + 20, 580);
        }
    }
}
