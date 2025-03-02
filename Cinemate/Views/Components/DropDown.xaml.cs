using CommunityToolkit.Maui.Views;
namespace Cinemate.Views.Components;

public partial class DropDown : ContentView
{
    public static readonly BindableProperty OptionsProperty =
        BindableProperty.Create(nameof(Options), typeof(List<string>), typeof(DropDown), default(List<string>), propertyChanged: OnOptionsChanged);

    public static readonly BindableProperty SelectedOptionProperty =
        BindableProperty.Create(nameof(SelectedOption), typeof(string), typeof(DropDown), default(string), BindingMode.TwoWay);

    public static readonly BindableProperty ColorBorderProperty =
        BindableProperty.Create(nameof(ColorBorder), typeof(Color), typeof(DropDown), Colors.Gray, propertyChanged: OnColorBorderChanged);

    public List<string> Options
    {
        get => (List<string>)GetValue(OptionsProperty);
        set => SetValue(OptionsProperty, value);
    }

    public string SelectedOption
    {
        get => (string)GetValue(SelectedOptionProperty);
        set
        {
            SetValue(SelectedOptionProperty, value);
            OnPropertyChanged(nameof(SelectedOption));
        }
    }

    public Color ColorBorder
    {
        get => (Color)GetValue(ColorBorderProperty);
        set => SetValue(ColorBorderProperty, value);
    }

    public DropDown()
    {
        InitializeComponent();
    }

    private static void OnOptionsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is DropDown dropdown)
        {
            dropdown.UpdateOptions();
        }
    }

    private static void OnColorBorderChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is DropDown dropdown)
        {
            dropdown.HeaderDropDown.BorderColor = (Color)newValue;
        }
    }

    private void UpdateOptions()
    {
        if (Options == null) return;

        var stack = this.FindByName<VerticalStackLayout>("OptionsStackLayout");
        if (stack == null) return;

        stack.Children.Clear();
        foreach (var option in Options)
        {
            var label = new Label
            {
                Text = option,
                BackgroundColor = Colors.Transparent
            };

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += OptionTapped;
            label.GestureRecognizers.Add(tapGesture);

            stack.Children.Add(label);
        }
    }

    private void Expander_ExpandedChanged(object sender, CommunityToolkit.Maui.Core.ExpandedChangedEventArgs e)
    {
        var control = sender as Expander;
        SwitchIcon(control.IsExpanded);
    }

    private void SwitchIcon(bool isExpanded)
    {
        if (isExpanded)
            HeaderIcon.Source = "up_arrow";
        else
            HeaderIcon.Source = "down_arrow";
    }

    private void OptionTapped(object sender, EventArgs e)
    {
        var tappedLabel = (Label)sender;
        SelectedOption = tappedLabel.Text;
        expander.IsExpanded = false;

        tappedLabel.BackgroundColor = Color.FromArgb("#E1E1E1");
        tappedLabel.FontAttributes = FontAttributes.Bold;
        tappedLabel.Padding = new Thickness(5);

        UpdateVisualState(tappedLabel);
    }

    private void UpdateVisualState(Label selectedLabel)
    {
        var stack = this.FindByName<VerticalStackLayout>("OptionsStackLayout");
        if (stack == null) return;

        foreach (var child in stack.Children)
        {
            if (child is Label label)
            {
                label.BackgroundColor = Colors.Transparent;
                label.FontAttributes = FontAttributes.None;
                label.Padding = new Thickness(0);
            }
        }

        selectedLabel.BackgroundColor = Color.FromArgb("#E1E1E1");
        selectedLabel.FontAttributes = FontAttributes.Bold;
        selectedLabel.Padding = new Thickness(5);

        HeaderLabel.Text = selectedLabel.Text;
    }

}