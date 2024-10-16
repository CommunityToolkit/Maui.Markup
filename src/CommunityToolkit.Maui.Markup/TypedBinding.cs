using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Controls.Xaml.Diagnostics;

namespace CommunityToolkit.Maui.Markup;

sealed class TypedBinding<TSource, TProperty> : TypedBindingBase where TSource : class?
{
	readonly WeakReference<TSource?> weakSource = new(null);
	readonly WeakReference<BindableObject?> weakTarget = new(null);

	readonly Func<TSource, TProperty> getter;
	readonly Action<TSource, TProperty>? setter;
	readonly PropertyChangedProxy[] handlers;
	readonly List<WeakReference<Element>> ancestryChain = [];

	bool isBindingContextRelativeSource;
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
	[MemberNotNull(nameof(targetProperty)), MemberNotNull(nameof(specificity))]
	internal override void Apply(object? context, BindableObject bindObj, BindableProperty targetProperty, bool fromBindingContextChanged, SetterSpecificity specificity)
	{
		this.targetProperty = targetProperty;
		this.specificity = specificity;
		var source = Source ?? Context ?? context;
		var isApplied = IsApplied;

		if (Source is not null && isApplied && fromBindingContextChanged)
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

		weakSource.SetTarget(source as TSource);
		weakTarget.SetTarget(bindObj);

		ApplyCore(source as TSource, bindObj, targetProperty, false, specificity);
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

	[return: NotNullIfNotNull(nameof(value))]
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

	internal override void ApplyToResolvedSource(object sourceObject, BindableObject target, BindableProperty property, bool fromTarget, SetterSpecificity specificity)
	{
		if (sourceObject is not TSource source)
		{
			throw new InvalidOperationException($"Invalid context. Expected type {typeof(TSource).FullName}, but received {sourceObject.GetType()?.FullName ?? "null"}");
		}

		weakTarget.SetTarget(target);
		weakSource.SetTarget(source);

		ApplyCore(source, target, property, fromTarget, specificity);
	}

	internal override void SubscribeToAncestryChanges(List<Element> chain, bool includeBindingContext, bool rootIsSource)
	{
		ArgumentNullException.ThrowIfNull(chain);

		ClearAncestryChangeSubscriptions();

		isBindingContextRelativeSource = includeBindingContext;
		ancestryChain.Clear();

		for (int i = 0; i < chain.Count; i++)
		{
			var elem = chain[i];
			if (i != chain.Count - 1 || !rootIsSource)
			{
				// don't care about a successfully resolved source's parents
				elem.ParentSet += OnElementParentSet;
			}
			if (isBindingContextRelativeSource)
			{
				elem.BindingContextChanged += OnElementBindingContextChanged;
			}

			ancestryChain.Add(new WeakReference<Element>(elem));
		}
	}

	void ClearAncestryChangeSubscriptions(int beginningWith = 0)
	{
		if (ancestryChain.Count is 0)
		{
			return;
		}

		var count = ancestryChain.Count;
		for (var i = beginningWith; i < count; i++)
		{
			var weakElement = ancestryChain.Last();
			if (weakElement.TryGetTarget(out var elem))
			{
				elem.ParentSet -= OnElementParentSet;
				if (isBindingContextRelativeSource)
				{
					elem.BindingContextChanged -= OnElementBindingContextChanged;
				}
			}
			ancestryChain.RemoveAt(ancestryChain.Count - 1);
		}
	}

	// ApplyCore is as slim as it should be:
	// Setting  100000 values						: 17ms.
	// ApplyCore  100000 (w/o INPC, w/o unapply)	: 20ms.
	void ApplyCore(TSource? sourceObject, BindableObject target, BindableProperty property, bool fromTarget, SetterSpecificity specificity)
	{
		var mode = this.GetRealizedMode(property);
		if (mode is BindingMode.OneWay or BindingMode.OneTime && fromTarget)
		{
			return;
		}

		var needsGetter = (mode == BindingMode.TwoWay && !fromTarget) || mode == BindingMode.OneWay || mode == BindingMode.OneTime;

		if (sourceObject is not null && mode is BindingMode.OneWay or BindingMode.TwoWay)
		{
			Subscribe(sourceObject);
		}

		if (needsGetter)
		{
			var value = FallbackValue ?? property.GetDefaultValue(target);
			if (sourceObject is not null)
			{
				try
				{
					var returnValue = getter(sourceObject);
					value = GetSourceValue(returnValue, property.ReturnType);
				}
				catch (Exception ex) when (ex is NullReferenceException or KeyNotFoundException or IndexOutOfRangeException or ArgumentOutOfRangeException)
				{
				}
			}
			if (!BindingExpressionHelper.TryConvert(ref value, property, property.ReturnType, true))
			{
				BindingDiagnostics.SendBindingFailure(this, sourceObject, target, property, "Binding", BindingExpression.CannotConvertTypeErrorMessage, value, property.ReturnType);
				return;
			}
			target.SetValueCore(property, value, SetValueFlags.ClearDynamicResource, BindableObject.SetValuePrivateFlags.Default | BindableObject.SetValuePrivateFlags.Converted, specificity);
			return;
		}

		var needsSetter = (mode == BindingMode.TwoWay && fromTarget) || mode == BindingMode.OneWayToSource;
		if (needsSetter && sourceObject is not null)
		{
			var value = GetTargetValue(target.GetValue(property), typeof(TProperty)) ?? throw new InvalidOperationException("Unable to find target value");

			if (!BindingExpressionHelper.TryConvert(ref value, property, typeof(TProperty), false))
			{
				BindingDiagnostics.SendBindingFailure(this, sourceObject, target, property, "Binding", BindingExpression.CannotConvertTypeErrorMessage, value, typeof(TProperty));
				return;
			}

			setter?.Invoke(sourceObject, (TProperty)value);
		}
	}

	// Returns null if the member is not in the chain or the
	// chain is no longer valid.
	int? FindAncestryIndex(Element elem)
	{
		for (var i = 0; i < ancestryChain.Count; i++)
		{
			var weak = ancestryChain[i];
			if (!weak.TryGetTarget(out var chainMember))
			{
				return -1;
			}

			if (Equals(elem, chainMember))
			{
				return i;
			}
		}

		return null;
	}

	void OnElementBindingContextChanged(object? sender, EventArgs e)
	{
		if (sender is not Element elem)
		{
			return;
		}

		if (!weakTarget.TryGetTarget(out var target) || targetProperty is null)
		{
			return;
		}

		if (weakSource.TryGetTarget(out var currentSource))
		{
			// make sure that this isn't just a repeat notice
			// from someone else in the chain about our already-resolved 
			// binding source
			if (ReferenceEquals(currentSource, elem.BindingContext))
			{
				return;
			}
		}

		Unapply();
		Apply(null, target, targetProperty, false, SetterSpecificity.FromBinding);
	}

	void OnElementParentSet(object? sender, EventArgs e)
	{
		if (sender is not Element element || !weakTarget.TryGetTarget(out var target) || targetProperty is null)
		{
			return;
		}

		if (element.Parent is null)
		{
			// Remove anything further up in the chain
			// than the element with the null parent
			var index = FindAncestryIndex(element);

			if (index is null)
			{
				Unapply();
				return;
			}

			if (index + 1 < ancestryChain.Count)
			{
				ClearAncestryChangeSubscriptions(index.Value + 1);
			}

			// Force the binding expression to resolve to null
			// for now, until someone in the chain gets a new
			// non-null parent.
			ApplyCore(null, target, targetProperty, false, specificity ?? default);
		}
		else
		{
			Unapply();
			Apply(null, target, targetProperty, false, specificity ?? default);
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
			get => Listener?.TryGetSource(out var target) is true ? target : null;
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