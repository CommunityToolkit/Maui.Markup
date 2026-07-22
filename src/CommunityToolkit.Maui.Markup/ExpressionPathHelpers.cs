using System.Linq.Expressions;

namespace CommunityToolkit.Maui.Markup;

static class ExpressionPathHelpers
{
	public static Func<TBindingContext, TSource> ConvertExpressionToFunc<TBindingContext, TSource>(Expression<Func<TBindingContext, TSource>> expression)
	{
		ArgumentNullException.ThrowIfNull(expression);

		return expression.Compile();
	}

	public static string GetMemberPath<TBindingContext>((Func<TBindingContext, object?>, string)[] handlers)
	{
		ArgumentNullException.ThrowIfNull(handlers);

		if (handlers.Length is 0)
		{
			throw new InvalidOperationException("Invalid handlers. The `handlers` parameter must include at least one member path segment.");
		}

		if (handlers.Any(static handler => string.IsNullOrWhiteSpace(handler.Item2)))
		{
			throw new InvalidOperationException("Invalid handlers. The `handlers` parameter cannot contain null/empty member names.");
		}

		return string.Join(".", handlers.Select(static handler => handler.Item2));
	}

	public static string? GetMemberPathOrNullForCapturedValue<T>(Expression<T>? expression)
	{
		if (expression is null)
		{
			return null;
		}

		var members = new Stack<string>();
		var currentExpression = UnwrapConvertExpression(expression.Body);

		while (currentExpression is MemberExpression memberExpression)
		{
			members.Push(memberExpression.Member.Name);
			currentExpression = UnwrapConvertExpression(memberExpression.Expression);
		}

		return currentExpression switch
		{
			ParameterExpression when members.Count > 0 => string.Join(".", members),
			// Getters rooted in a captured variable (`ConstantExpression`, e.g. `_ => capturedViewModel.HeightRequest`)
			// or in a static member (`null`, e.g. `_ => Colors.Black`) do not reference the binding context.
			// Both are intentionally treated as captured values: the binding has no member path and evaluates
			// the compiled getter as a one-time constant. Setters remain unsupported for these getters because
			// there is no resolvable member path for source write-back.
			ConstantExpression when members.Count > 0 => null,
			null when members.Count > 0 => null,
			_ => throw CreateInvalidGetterException()
		};
	}

	public static InvalidOperationException CreateInvalidGetterException()
		=> new("Invalid getter. The `getter` parameter must point to a property path in the ViewModel and cannot add additional logic");

	static Expression? UnwrapConvertExpression(Expression? expression)
	{
		while (expression is UnaryExpression { NodeType: ExpressionType.Convert or ExpressionType.ConvertChecked } unaryExpression)
		{
			expression = unaryExpression.Operand;
		}

		return expression;
	}
}
