using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AsyncAwaitBestPractices;

namespace CommunityToolkit.Maui.Markup.Sample.ViewModels.Base;

abstract class BaseViewModel : INotifyPropertyChanged
{
	readonly WeakEventManager propertyChangedEventManager = new();

	event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
	{
		add => propertyChangedEventManager.AddEventHandler(value);
		remove => propertyChangedEventManager.RemoveEventHandler(value);
	}

	protected void SetProperty<T>(ref T backingStore, in T value, in Action? onChanged = null, [CallerMemberName] in string propertyname = "")
	{
		if (EqualityComparer<T>.Default.Equals(backingStore, value))
		{
			return;
		}

		backingStore = value;

		onChanged?.Invoke();

		OnPropertyChanged(propertyname);
	}

	void OnPropertyChanged([CallerMemberName] in string propertyName = "") =>
		propertyChangedEventManager.RaiseEvent(this, new PropertyChangedEventArgs(propertyName), nameof(INotifyPropertyChanged.PropertyChanged));
}