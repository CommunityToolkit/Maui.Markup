using System.Linq.Expressions;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using NUnit.Framework;
namespace CommunityToolkit.Maui.Markup.UnitTests;

[TestFixture]
class ExpressionPathHelpersTests : BaseTestFixture
{
	const string invalidGetterMessage = "Invalid getter. The `getter` parameter must point to a property path in the ViewModel and cannot add additional logic";
	const string emptyHandlersMessage = "Invalid handlers. The `handlers` parameter must include at least one member path segment.";
	const string invalidMemberNamesMessage = "Invalid handlers. The `handlers` parameter cannot contain null/empty member names.";

	[Test]
	public void ConvertExpressionToFuncThrowsArgumentNullExceptionForNullExpression()
	{
		Assert.Throws<ArgumentNullException>(() => ExpressionPathHelpers.ConvertExpressionToFunc<TestViewModel, double>(null!));
	}

	[Test]
	public void ConvertExpressionToFuncCompilesGetter()
	{
		var viewModel = new TestViewModel
		{
			HeightRequest = 123.45
		};

		var func = ExpressionPathHelpers.ConvertExpressionToFunc<TestViewModel, double>(static vm => vm.HeightRequest);

		Assert.That(func(viewModel), Is.EqualTo(123.45));
	}

	[Test]
	public void GetMemberPathThrowsArgumentNullExceptionForNullHandlers()
	{
		Assert.Throws<ArgumentNullException>(() => ExpressionPathHelpers.GetMemberPath<TestViewModel>(null!));
	}

	[Test]
	public void GetMemberPathThrowsInvalidOperationExceptionForEmptyHandlers()
	{
		var exception = Assert.Throws<InvalidOperationException>(() => ExpressionPathHelpers.GetMemberPath<TestViewModel>([]));

		Assert.That(exception?.Message, Is.EqualTo(emptyHandlersMessage));
	}

	[TestCase(null)]
	[TestCase("")]
	[TestCase(" ")]
	[TestCase("\t")]
	public void GetMemberPathThrowsInvalidOperationExceptionForInvalidMemberNames(string? invalidMemberName)
	{
		var exception = Assert.Throws<InvalidOperationException>(() => ExpressionPathHelpers.GetMemberPath<TestViewModel>(
		[
			(static vm => vm.Nested, nameof(TestViewModel.Nested)),
			(static vm => vm, invalidMemberName!)
		]));

		Assert.That(exception?.Message, Is.EqualTo(invalidMemberNamesMessage));
	}

	[Test]
	public void GetMemberPathReturnsSingleSegment()
	{
		var path = ExpressionPathHelpers.GetMemberPath<TestViewModel>(
		[
			(static vm => vm.Nested, nameof(TestViewModel.Nested))
		]);

		Assert.That(path, Is.EqualTo(nameof(TestViewModel.Nested)));
	}

	[Test]
	public void GetMemberPathJoinsSegmentsInOrder()
	{
		var path = ExpressionPathHelpers.GetMemberPath<TestViewModel>(
		[
			(static vm => vm, "First"),
			(static vm => vm, "Second"),
			(static vm => vm, "Third")
		]);

		Assert.That(path, Is.EqualTo("First.Second.Third"));
	}

	[Test]
	public void GetMemberPathOrNullReturnsNullForNullExpression()
	{
		Assert.That(ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue<Func<TestViewModel, double>>(null), Is.Null);
	}

	[Test]
	public void SimplePropertyGetterReturnsMemberPath()
	{
		Expression<Func<TestViewModel, double>> getter = static vm => vm.HeightRequest;

		Assert.That(ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter), Is.EqualTo(nameof(TestViewModel.HeightRequest)));
	}

	[Test]
	public void NestedPropertyGetterReturnsDottedMemberPath()
	{
		Expression<Func<TestViewModel, string>> getter = static vm => vm.Nested.Id;

		Assert.That(ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter), Is.EqualTo($"{nameof(TestViewModel.Nested)}.{nameof(NestedViewModel.Id)}"));
	}

	[Test]
	public void BoxingGetterUnwrapsConversionAndReturnsMemberPath()
	{
		Expression<Func<TestViewModel, object>> getter = static vm => vm.HeightRequest;

		Assert.That(ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter), Is.EqualTo(nameof(TestViewModel.HeightRequest)));
	}

	[Test]
	public void StackedConversionsGetterUnwrapsAllConversions()
	{
		Expression<Func<TestViewModel, object>> getter = static vm => (object)(double)vm.Count;

		Assert.That(ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter), Is.EqualTo(nameof(TestViewModel.Count)));
	}

	[Test]
	public void CheckedConversionGetterUnwrapsConversion()
	{
		var parameter = Expression.Parameter(typeof(TestViewModel), "vm");
		var heightRequestProperty = Expression.Property(parameter, nameof(TestViewModel.HeightRequest));
		var getter = Expression.Lambda<Func<TestViewModel, int>>(Expression.ConvertChecked(heightRequestProperty, typeof(int)), parameter);

		Assert.That(ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter), Is.EqualTo(nameof(TestViewModel.HeightRequest)));
	}

	[Test]
	public void CastWithinMemberChainUnwrapsConversion()
	{
		Expression<Func<TestViewModel, string>> getter = static vm => ((NestedViewModel)vm.NestedAsObject).Id;

		Assert.That(ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter), Is.EqualTo($"{nameof(TestViewModel.NestedAsObject)}.{nameof(NestedViewModel.Id)}"));
	}

	[Test]
	public void CapturedLocalVariableGetterReturnsNull()
	{
		var captured = new TestViewModel();
		Expression<Func<TestViewModel, double>> getter = _ => captured.HeightRequest;

		Assert.That(ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter), Is.Null);
	}

	[Test]
	public void CapturedLocalValueGetterReturnsNull()
	{
		var captured = new TestViewModel();
		Expression<Func<TestViewModel, TestViewModel>> getter = _ => captured;

		Assert.That(ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter), Is.Null);
	}

	[Test]
	public void StaticFieldGetterReturnsNull()
	{
		Expression<Func<TestViewModel, Color>> getter = static _ => Colors.Red;

		Assert.That(ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter), Is.Null);
	}

	[Test]
	public void StaticPropertyGetterReturnsNull()
	{
		Expression<Func<TestViewModel, string>> getter = static _ => Environment.NewLine;

		Assert.That(ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter), Is.Null);
	}

	[Test]
	public void NestedStaticMemberGetterReturnsNull()
	{
		Expression<Func<TestViewModel, int>> getter = static _ => Environment.NewLine.Length;

		Assert.That(ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter), Is.Null);
	}

	[Test]
	public void IdentityGetterThrowsInvalidGetterException()
	{
		Expression<Func<TestViewModel, TestViewModel>> getter = static vm => vm;

		var exception = Assert.Throws<InvalidOperationException>(() => ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter));

		Assert.That(exception?.Message, Is.EqualTo(invalidGetterMessage));
	}

	[Test]
	public void ConstantLiteralGetterThrowsInvalidGetterException()
	{
		Expression<Func<TestViewModel, double>> getter = static _ => 12.0;

		var exception = Assert.Throws<InvalidOperationException>(() => ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter));

		Assert.That(exception?.Message, Is.EqualTo(invalidGetterMessage));
	}

	[Test]
	public void MethodCallGetterThrowsInvalidGetterException()
	{
		Expression<Func<TestViewModel, string?>> getter = static vm => vm.ToString();

		var exception = Assert.Throws<InvalidOperationException>(() => ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter));

		Assert.That(exception?.Message, Is.EqualTo(invalidGetterMessage));
	}

	[Test]
	public void NegationGetterThrowsInvalidGetterException()
	{
		Expression<Func<TestViewModel, double>> getter = static vm => -vm.HeightRequest;

		var exception = Assert.Throws<InvalidOperationException>(() => ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter));

		Assert.That(exception?.Message, Is.EqualTo(invalidGetterMessage));
	}

	[Test]
	public void BinaryExpressionGetterThrowsInvalidGetterException()
	{
		Expression<Func<TestViewModel, double>> getter = static vm => vm.HeightRequest + 1;

		var exception = Assert.Throws<InvalidOperationException>(() => ExpressionPathHelpers.GetMemberPathOrNullForCapturedValue(getter));

		Assert.That(exception?.Message, Is.EqualTo(invalidGetterMessage));
	}

	[Test]
	public void CreateInvalidGetterExceptionReturnsInvalidOperationExceptionWithContractMessage()
	{
		var exception = ExpressionPathHelpers.CreateInvalidGetterException();

		Assert.That(exception.Message, Is.EqualTo(invalidGetterMessage));
	}

	sealed class TestViewModel
	{
		public double HeightRequest { get; set; }

		public int Count { get; set; }

		public NestedViewModel Nested { get; } = new();

		public object NestedAsObject { get; } = new NestedViewModel();
	}

	sealed class NestedViewModel
	{
		public string Id { get; set; } = string.Empty;
	}
}