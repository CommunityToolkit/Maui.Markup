namespace CommunityToolkit.Maui.Markup.Sample.Pages.Base;

abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel
{
	protected BaseContentPage(T viewModel, string pageTitle)
	{
		base.BindingContext = viewModel;

		Title = pageTitle;
		this.SetAppThemeColor(ContentPage.BackgroundColorProperty, Color.FromArgb("F6F6EF"), Color.FromArgb("1B1B1B"));
	}

	protected new T BindingContext => (T)base.BindingContext;
}