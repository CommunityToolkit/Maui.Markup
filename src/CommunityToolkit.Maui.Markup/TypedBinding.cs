using System.ComponentModel;
using System.Globalization;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Controls.Xaml.Diagnostics;

namespace CommunityToolkit.Maui.Markup;

sealed class TypedBinding<TSource, TProperty> : TypedBindingBase
{
	readonly WeakReference<object?> weakSource = new(null);
	readonly WeakReference<BindableObject?> weakTarget = new(null);

	readonly Func<TSource, TProperty> getter;
	readonly Action<TSource, TProperty>? setter;
	readonly PropertyChangedProxy[] handlers;

	SetterSpecificity? specificity;
	BindableProperty? targetProperty;

	public TypedBinding(Func<TSource, TProperty> getter, Action<TSource, TProperty>? setter, (Func<TSource, object?>, string)[] handlers)
	{
		ArgumentNullException.ThrowIfNull(handlers);

		this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
		this.setter = setter;

		this.handlers = new PropertyChangedProxy[handlers.Length];
		for (var i = 0; i < handlers.Length; i++)
		{
			this.handlers[i] = new PropertyChangedProxy(handlers[i].Item1, handlers[i].Item2, this);
		}
	}

	// Applies the binding to a previously set source and target.
	internal override void Apply(bool fromTarget = false)
	{
		base.Apply(fromTarget);

		if (!weakTarget.TryGetTarget(out var target))
		{
			Unapply();
			return;
		}

		if (weakSource.TryGetTarget(out var source)
			&& targetProperty is not null
			&& specificity.HasValue)
		{
			ApplyCore(source, target, targetProperty, fromTarget, specificity.Value);
		}
	}

	// Applies the binding to a new source or target.
	internal override void Apply(object context, BindableObject bindObj, BindableProperty targetProperty, bool fromBindingContextChanged, SetterSpecificity specificity)
	{
		this.targetProperty = targetProperty;
		this.specificity = specificity;
		var source = Source ?? Context ?? context;
		var isApplied = IsApplied;

		if (Source != null && isApplied && fromBindingContextChanged)
		{
			return;
		}

		base.Apply(source, bindObj, targetProperty, fromBindingContextChanged, specificity);

		if (weakTarget.TryGetTarget(out var prevTarget) && !ReferenceEquals(prevTarget, bindObj))
		{
			throw new InvalidOperationException("Binding instances cannot be reused");
		}

		if (weakSource.TryGetTarget(out var previousSource) && !ReferenceEquals(previousSource, source))
		{
			throw new InvalidOperationException("Binding instances cannot be reused");
		}

		weakSource.SetTarget(source);
		weakTarget.SetTarget(bindObj);

		ApplyCore(source, bindObj, targetProperty, false, specificity);
	}

	internal override BindingBase Clone()
	{
		var handlers = new ValueTuple<Func<TSource, object?>, string>[this.handlers.Length];

		for (var i = 0; i < this.handlers.Length; i++)
		{
			handlers[i] = new ValueTuple<Func<TSource, object?>, string>(this.handlers[i].PartGetter, this.handlers[i].PropertyName);
		}

		return new TypedBinding<TSource, TProperty>(getter, setter, handlers)
		{
			Mode = Mode,
			Converter = Converter,
			ConverterParameter = ConverterParameter,
			StringFormat = StringFormat,
			Source = Source,
			UpdateSourceEventName = UpdateSourceEventName,
		};
	}

	internal override object GetSourceValue(object? value, Type targetPropertyType)
	{
		if (Converter is not null)
		{
			value = Converter.Convert(value, targetPropertyType, ConverterParameter, CultureInfo.CurrentUICulture);
		}

		return base.GetSourceValue(value, targetPropertyType);
	}

	internal override object? GetTargetValue(object? value, Type sourcePropertyType)
	{
		if (Converter is not null)
		{
			value = Converter.ConvertBack(value, sourcePropertyType, ConverterParameter, CultureInfo.CurrentUICulture);
		}

		//return base.GetTargetValue(value, sourcePropertyType);
		return value;
	}

	internal override void Unapply(bool fromBindingContextChanged = false)
	{
		if (Source != null && fromBindingContextChanged && IsApplied)
		{
			return;
		}

		base.Unapply(fromBindingContextChanged: fromBindingContextChanged);

		Unsubscribe();

		weakSource.SetTarget(null);
		weakTarget.SetTarget(null);
	}

	// ApplyCore is as slim as it should be:
	// Setting  100000 values						: 17ms.
	// ApplyCore  100000 (w/o INPC, w/o unapply)	: 20ms.
	void ApplyCore(object sourceObject, BindableObject target, BindableProperty property, bool fromTarget, SetterSpecificity specificity)
	{
		var isTSource = sourceObject is TSource;
		var mode = this.GetRealizedMode(property);
		if (mode is BindingMode.OneWay or BindingMode.OneTime && fromTarget)
		{
			return;
		}

		var needsGetter = (mode == BindingMode.TwoWay && !fromTarget) || mode == BindingMode.OneWay || mode == BindingMode.OneTime;

		if (isTSource && mode is BindingMode.OneWay or BindingMode.TwoWay)
		{
			Subscribe((TSource)sourceObject);
		}

		if (needsGetter)
		{
			var value = FallbackValue ?? property.GetDefaultValue(target);
			if (isTSource)
			{
				try
				{
					var returnValue = getter((TSource)sourceObject);
					value = GetSourceValue(returnValue, property.ReturnType);
				}
				catch (Exception ex) when (ex is NullReferenceException or KeyNotFoundException or IndexOutOfRangeException or ArgumentOutOfRangeException)
				{
				}
			}
			if (!BindingExpression.TryConvert(ref value, property, property.ReturnType, true))
			{
				BindingDiagnostics.SendBindingFailure(this, sourceObject, target, property, "Binding", BindingExpression.CannotConvertTypeErrorMessage, value, property.ReturnType);
				return;
			}
			target.SetValueCore(property, value, SetValueFlags.ClearDynamicResource, BindableObject.SetValuePrivateFlags.Default | BindableObject.SetValuePrivateFlags.Converted, specificity);
			return;
		}

		var needsSetter = (mode == BindingMode.TwoWay && fromTarget) || mode == BindingMode.OneWayToSource;
		if (needsSetter && isTSource)
		{
			var value = GetTargetValue(target.GetValue(property), typeof(TProperty));
			if (!BindingExpression.TryConvert(ref value, property, typeof(TProperty), false))
			{
				BindingDiagnostics.SendBindingFailure(this, sourceObject, target, property, "Binding", BindingExpression.CannotConvertTypeErrorMessage, value, typeof(TProperty));
				return;
			}
			setter?.Invoke((TSource)sourceObject, (TProperty)value);
		}
	}

	sealed class PropertyChangedProxy
	{
		readonly BindingBase binding;
		readonly PropertyChangedEventHandler handler;

		public PropertyChangedProxy(Func<TSource, object?> partGetter, string propertyName, BindingBase binding)
		{
			this.binding = binding;

			PartGetter = partGetter;
			PropertyName = propertyName;
			Listener = new BindingExpression.WeakPropertyChangedProxy();
			//avoid GC collection, keep a ref to the OnPropertyChanged handler
			handler = OnPropertyChanged;
		}

		~PropertyChangedProxy() => Listener?.Unsubscribe();

		public Func<TSource, object?> PartGetter { get; }
		public string PropertyName { get; }
		public BindingExpression.WeakPropertyChangedProxy? Listener { get; }

		public INotifyPropertyChanged? Part
		{
			get
			{
				return Listener?.TryGetSource(out var target) is true ? target : null;
			}
			set
			{
				//Already subscribed
				if (Listener?.TryGetSource(out var source) is true
					&& ReferenceEquals(value, source))
				{
					return;
				}

				//clear out previous subscription
				Listener?.Unsubscribe();

				if (value is not null)
				{
					Listener?.Subscribe(value, handler);
				}
			}
		}

		void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			ArgumentNullException.ThrowIfNull(sender);

			if (!string.IsNullOrEmpty(e.PropertyName)
				&& string.CompareOrdinal(e.PropertyName, PropertyName) is not 0)
			{
				return;
			}

			if (IPlatformApplication.Current?.Services.GetRequiredApplicationDispatcher() is IDispatcher dispatcher)
			{
				dispatcher.DispatchIfRequired(() => binding.Apply(false));
			}
			else
			{
				binding.Apply(false);
			}
		}
	}

	void Subscribe(TSource sourceObject)
	{
		foreach (var handler in handlers)
		{
			var part = handler.PartGetter(sourceObject);
			if (part is null)
			{
				break;
			}

			if (part is not INotifyPropertyChanged notifyPropertyChanged)
			{
				continue;
			}

			handler.Part = notifyPropertyChanged;
		}
	}

	void Unsubscribe()
	{
		foreach (var handle in handlers)
		{
			handle.Listener?.Unsubscribe();
		}
	}
}