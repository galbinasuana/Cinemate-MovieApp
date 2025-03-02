using System.Globalization;

namespace Cinemate.Models
{
    public class BooleanToColorConverter : IValueConverter
    {
        public Color TrueColor { get; set; }
        public Color FalseColor { get; set; }

        public BooleanToColorConverter()
        {
            FalseColor = Color.Parse("#E1E1E1");
            TrueColor = Color.Parse("#C393C1");
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSelected = (bool)value;
            return isSelected ? TrueColor : FalseColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
