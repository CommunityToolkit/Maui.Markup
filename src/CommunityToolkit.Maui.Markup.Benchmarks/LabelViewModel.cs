using CommunityToolkit.Mvvm.ComponentModel;

namespace CommunityToolkit.Maui.Markup.Benchmarks;

partial class LabelViewModel : ObservableObject
{
	[ObservableProperty]
	string text = string.Empty;

	[ObservableProperty]
	Color textColor = Colors.Black;
}