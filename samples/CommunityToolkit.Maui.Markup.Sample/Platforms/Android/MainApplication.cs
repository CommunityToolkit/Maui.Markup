using System;
using Android.App;
using Android.Runtime;
using Microsoft.Maui;

namespace CommunityToolkit.Maui.Markup.Sample
{
	[Application]
	public class MainApplication : MauiApplication
	{
		public MainApplication(IntPtr handle, JniHandleOwnership ownership)
			: base(handle, ownership)
		{
		}

		protected override MauiApp CreateMauiApp() => Startup.Create();
    }
}