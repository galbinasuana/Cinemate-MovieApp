using Cinemate.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace Cinemate.ViewModels
{
    public partial class AddMovieToMyListViewModel : ObservableObject, INotifyPropertyChanged
    {
        private DaoMovie daoMovie = DaoMovie.GetDaoMovie();

        public Dictionary<string,bool> CategorySelections { get; set; }
        public List<string> MovieOptions { get; set; }

        public AddMovieToMyListViewModel()
        {
            MovieOptions = PickerOptions.MovieStatuses;

            CategorySelections = new Dictionary<string, bool>
            {
                { "Action", false },
                { "Adventure", false },
                { "Animation", false },
                { "Comedy", false },
                { "Crime", false },
                { "Drama", false },
                { "Family", false },
                { "History", false },
                { "Horror", false },
                { "Music", false },
                { "Mystery", false },
                { "Romance", false }
            };
        }


        [ObservableProperty]
        private ImageSource selectedImageSource;
       
        private FileResult selectedImageFile;

        [RelayCommand]
        private async Task PickImage()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Pick Image",
                FileTypes = FilePickerFileType.Images
            });

            if (result == null)
                return;

            selectedImageFile = result;

            SelectedImageSource = ImageSource.FromStream(() =>
            {
                var stream = selectedImageFile.OpenReadAsync().Result;
                return stream;
            });
        }


        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string summary;

        [ObservableProperty]
        private string myReview;

        [ObservableProperty]
        private DateTime startDate = DateTime.Today;

        [ObservableProperty]
        private string selectedStatus;

        private bool isRatingSet = false;
        private double rating;
        public double Rating
        {
            get => rating;
            set
            {
                SetProperty(ref rating, NormalizeRating(value));
                isRatingSet = true;  
            }
        }

        private double NormalizeRating(double inputRating)
        {
            double normalizedRating = Math.Abs(inputRating);

            while (normalizedRating > 10)
            {
                normalizedRating -= 10;
            }

            return Math.Round(normalizedRating, 1);
        }

        public List<string> GetSelectedCategories()
        {
            return CategorySelections.Where(pair => pair.Value).Select(pair => pair.Key).ToList();
        }

        [RelayCommand]
        public async Task SubmitMovie()
        {
            var selectedCategories = GetSelectedCategories();
            var random = new Random();

            if (string.IsNullOrWhiteSpace(Title) || !isRatingSet || string.IsNullOrWhiteSpace(Summary) || string.IsNullOrWhiteSpace(MyReview) ||
            string.IsNullOrWhiteSpace(SelectedStatus) || selectedCategories == null || selectedCategories.Count == 0 || selectedImageFile == null)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Please ensure all fields including image, genre, title, rating, summary, and review are filled out and at least one genre is selected.", "OK");
                return;
            }


            var newMovie = new MovieLibrary
            {
                Title = this.Title,
                Cover = ImageConverter.ImageToBase64(selectedImageFile),
                Rating = this.Rating,
                Reviews = random.Next(1000, 5000),
                Metascore = random.Next(50, 100),
                CriticReviews = random.Next(100, 500),
                StartDate = this.StartDate.ToString("dd-MM-yyyy"),
                Status = this.SelectedStatus,
                Summary = this.Summary,
                MyReview = this.MyReview,
                Categories = selectedCategories              
            };

            await daoMovie.AddMovie(newMovie);
            Console.WriteLine("Movie added successfully.");

            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        void ToggleCategory(string categoryName)
        {
            if (CategorySelections.ContainsKey(categoryName))
            {
                CategorySelections[categoryName] = !CategorySelections[categoryName];
                OnPropertyChanged(nameof(CategorySelections));
            }
        }

        
        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
