using CommunityToolkit.Mvvm.ComponentModel;

namespace Cinemate.Models
{
    public class FilterOption : ObservableObject
    {
        public string Name { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }
    }
}
