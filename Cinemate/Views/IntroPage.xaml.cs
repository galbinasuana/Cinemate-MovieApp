using Cinemate.Models;

namespace Cinemate.Views
{
    public partial class IntroPage : ContentPage
    {
        IDispatcherTimer timer;
        int imgIndex01;
        int imgIndex02;
        int imgIndex03;

        public IntroPage()
        {
            InitializeComponent();
        }

        public IntroPage(IntroViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            timer = Application.Current.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += OnDispatcherTimer;
            timer.IsRepeating = true;
            timer.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            timer.Tick -= OnDispatcherTimer;
            timer.Stop();
        }

        async void OnDispatcherTimer(object sender, EventArgs e)
        {
            var rnd = new Random();

            imgIndex01 += rnd.Next(10, 50);
            imgIndex02 += rnd.Next(10, 50);
            imgIndex03 += rnd.Next(10, 50);

            await UpdateScrollPositions(imgIndex01, imgIndex02, imgIndex03);
            await Shell.Current.GoToAsync("//AboutPage");
        }

        async Task UpdateScrollPositions(double scrollPosition1, double scrollPosition2, double scrollPosition3)
        {
            await ImgCollectionView01.ScrollToAsync(0, scrollPosition1, true);
            await ImgCollectionView02.ScrollToAsync(0, scrollPosition2, true);
            await ImgCollectionView03.ScrollToAsync(0, scrollPosition3, true);
        }
    }
}
