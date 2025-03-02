using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;


namespace Cinemate.Models
{
    public partial class IntroViewModel : ObservableObject
    {
        readonly ImageCollection imageCollection;

        public IntroViewModel(ImageCollection imgCollection)
        {
            imageCollection = imgCollection;

            ImgCollection01 = new ObservableCollection<string>();
            ImgCollection02 = new ObservableCollection<string>();
            ImgCollection03 = new ObservableCollection<string>();

            LoadData();
        }

        public ObservableCollection<string> ImgCollection01 { get; set; }
        public ObservableCollection<string> ImgCollection02 { get; set; }
        public ObservableCollection<string> ImgCollection03 { get; set; }

        [RelayCommand]
        void LoadMoreArtCollection01()
        {
            var imgCollection01 = imageCollection.GetImgCollection01();

            foreach (var item in imgCollection01)
            {
                ImgCollection01.Add(item);
            }
        }

        [RelayCommand]
        void LoadMoreArtCollection02()
        {
            var imgCollection02 = imageCollection.GetImgCollection02();

            foreach (var item in imgCollection02)
            {
                ImgCollection02.Add(item);
            }
        }

        [RelayCommand]
        void LoadMoreArtCollection03()
        {
            var imgCollection03 = imageCollection.GetImgCollection03();

            foreach (var item in imgCollection03)
            {
                ImgCollection03.Add(item);
            }
        }

        void LoadData()
        {
            var imgCollection01 = imageCollection.GetImgCollection01();
            foreach (var item in imgCollection01)
            {
                ImgCollection01.Add(item);
            }

            var imgCollection02 = imageCollection.GetImgCollection02();
            foreach (var item in imgCollection02)
            {
                ImgCollection02.Add(item);
            }

            var imgCollection03 = imageCollection.GetImgCollection03();
            foreach (var item in imgCollection03)
            {
                ImgCollection03.Add(item);
            }
        }
    }
}