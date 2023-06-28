using System.Diagnostics.CodeAnalysis;

namespace CommunityToolkit.Maui.Markup.Sample;

class HotReloadHandler : ICommunityToolkitHotReloadHandler
{
	public async void OnHotReload(IReadOnlyList<Type> types)
	{
		if (Application.Current?.MainPage is not Page mainPage)
		{
			return;
		}

		foreach (var type in types)
		{
			if (type.IsSubclassOf(typeof(Page)))
			{
				if (mainPage is AppShell shell)
				{
					var visiblePage = shell.CurrentPage;

					await mainPage.Dispatcher.DispatchAsync(async () =>
					{
						await Shell.Current.GoToAsync(type.Name, false);
						Shell.Current.Navigation.RemovePage(visiblePage);
					});

					break;
				}
				else
				{
					if (TryGetModalStackPage(out var modalPage))
					{
						await mainPage.Dispatcher.DispatchAsync(async () =>
						{
							await mainPage.Navigation.PopModalAsync(false);
							await mainPage.Navigation.PushModalAsync(modalPage, false);
						});
					}
					else
					{
						await mainPage.Dispatcher.DispatchAsync(async () =>
						{
							await mainPage.Navigation.PopAsync(false);
							await mainPage.Navigation.PushAsync(modalPage, false);
						});
					}

					break;
				}
			}
		}
	}


	static bool TryGetModalStackPage([NotNullWhen(true)] out Page? page)
	{
		page = Application.Current?.MainPage?.Navigation.ModalStack.LastOrDefault();
		return page is not null;
	}
}