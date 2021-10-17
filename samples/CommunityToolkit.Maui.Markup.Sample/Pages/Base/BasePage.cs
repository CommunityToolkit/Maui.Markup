using CommunityToolkit.Maui.Markup.Sample.ViewModels.Base;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup.Sample.Pages.Base;

abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel
{
    protected BaseContentPage(T viewModel, string pageTitle)
    {
        BindingContext = ViewModel = viewModel;
        Title = pageTitle;
    }

    protected T ViewModel { get; }
}
