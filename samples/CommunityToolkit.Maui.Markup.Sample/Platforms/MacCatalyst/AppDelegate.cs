﻿using System.Diagnostics.CodeAnalysis;
using Foundation;
namespace CommunityToolkit.Maui.Markup.Sample;

[Register(nameof(AppDelegate))]
[RequiresUnreferencedCode("SettingsViewModel Calls CommunityToolkit.Maui.Behaviors.NumericValidationBehavior.NumericValidationBehavior()")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}