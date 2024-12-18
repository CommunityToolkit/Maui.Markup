using CommunityToolkit.Mvvm.ComponentModel;
namespace CommunityToolkit.Maui.Markup.Benchmarks;

public partial class LabelViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string Text { get; set; } = string.Empty;

    [ObservableProperty]
    public partial Color TextColor { get; set; } = Colors.Black;
}