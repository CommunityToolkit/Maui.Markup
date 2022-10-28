using CommunityToolkit.Maui.Markup.UnitTests.Base;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using NUnit.Framework;

namespace CommunityToolkit.Maui.Markup.UnitTests
{
	[TestFixture]
	class TextAlignmentExtensionsTests : BaseMarkupTestFixture<Picker>
	{
		[Test]
		public void TextStart()
			=> TestPropertiesSet(l => l.TextStart(), (TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.End, TextAlignment.Start));

		[Test]
		public void TextCenterHorizontal()
			=> TestPropertiesSet(l => l.TextCenterHorizontal(), (TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

		[Test]
		public void TextEnd()
			=> TestPropertiesSet(l => l.TextEnd(), (TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.End));

		[Test]
		public void TextTop()
			=> TestPropertiesSet(l => l.TextTop(), (TextAlignmentElement.VerticalTextAlignmentProperty, TextAlignment.End, TextAlignment.Start));

		[Test]
		public void TextCenterVertical()
			=> TestPropertiesSet(l => l.TextCenterVertical(), (TextAlignmentElement.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

		[Test]
		public void TextBottom()
			=> TestPropertiesSet(l => l.TextBottom(), (TextAlignmentElement.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.End));

		[Test]
		public void TextCenter()
			=> TestPropertiesSet(
					l => l.TextCenter(),
					(TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center),
					(TextAlignmentElement.VerticalTextAlignmentProperty, TextAlignment.Start, TextAlignment.Center));

		[Test]
		public void SupportDerivedFromBindable()
		{
			Assert.IsInstanceOf<DerivedFromSearchBar>(
				new DerivedFromSearchBar()
				.TextStart()
				.TextCenterHorizontal()
				.TextEnd()
				.TextTop()
				.TextCenterVertical()
				.TextBottom()
				.TextCenter()
				.FontSize(8.0)
				.Bold()
				.Italic());
		}

		[Test]
		public void SupportCustomTextAlignment()
		{
			Assert.IsInstanceOf<CustomTextAlignmentControl>(
				new CustomTextAlignmentControl()
				.TextStart()
				.TextCenterHorizontal()
				.TextEnd()
				.TextTop()
				.TextCenterVertical()
				.TextBottom()
				.TextCenter());
		}

		class DerivedFromSearchBar : SearchBar { }
	}

	class CustomTextAlignmentControl : ITextAlignment
	{
		public TextAlignment HorizontalTextAlignment { get; set; } = TextAlignment.Center;

		public TextAlignment VerticalTextAlignment { get; set; } = TextAlignment.Center;
	}
}

namespace CommunityToolkit.Maui.Markup.UnitTests
{
	using CommunityToolkit.Maui.Markup.LeftToRight;

	[TestFixture]
	class LeftToRightTextAlignmentExtensionsTests : BaseMarkupTestFixture<Picker>
	{

		[Test]
		public void TextLeft()
			=> TestPropertiesSet(l => l.TextLeft(), (TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start));

		[Test]
		public void TextRight()
			=> TestPropertiesSet(l => l.TextRight(), (TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.End));

		[Test]
		public void SupportDerivedFromBindable()
		{
			Assert.IsInstanceOf<DerivedFromEntry>(
				new DerivedFromEntry()
				.TextStart()
				.TextCenterHorizontal()
				.TextEnd()
				.TextTop()
				.TextCenterVertical()
				.TextBottom()
				.TextCenter()
				.FontSize(8.0)
				.Bold()
				.Italic());
		}

		[Test]
		public void SupportCustomTextAlignment()
		{
			Assert.IsInstanceOf<LeftToRightCustomTextAlignmentControl>(
				new LeftToRightCustomTextAlignmentControl()
				.TextStart()
				.TextCenterHorizontal()
				.TextEnd()
				.TextTop()
				.TextCenterVertical()
				.TextBottom()
				.TextCenter());
		}

		class DerivedFromEntry : Entry { }
	}

	class LeftToRightCustomTextAlignmentControl : ITextAlignment
	{
		public TextAlignment HorizontalTextAlignment { get; set; } = TextAlignment.Center;

		public TextAlignment VerticalTextAlignment { get; set; } = TextAlignment.Center;
	}
}

namespace CommunityToolkit.Maui.Markup.UnitTests
{
	using CommunityToolkit.Maui.Markup.RightToLeft;

	[TestFixture]
	class RightToLeftTextAlignmentExtensionsTests : BaseMarkupTestFixture<Picker>
	{

		[Test]
		public void TextLeft()
			=> TestPropertiesSet(l => l.TextLeft(), (TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.End));

		[Test]
		public void TextRight()
			=> TestPropertiesSet(l => l.TextRight(), (TextAlignmentElement.HorizontalTextAlignmentProperty, TextAlignment.Start));

		[Test]
		public void SupportDerivedFromBindable()
		{
			Assert.IsInstanceOf<DerivedFromEditor>(
				new DerivedFromEditor()
				.TextStart()
				.TextCenterHorizontal()
				.TextEnd()
				.TextTop()
				.TextCenterVertical()
				.TextBottom()
				.TextCenter()
				.FontSize(8.0)
				.Bold()
				.Italic());
		}

		[Test]
		public void SupportCustomTextAlignment()
		{
			Assert.IsInstanceOf<RightToLeftCustomTextAlignmentControl>(
				new RightToLeftCustomTextAlignmentControl()
				.TextStart()
				.TextCenterHorizontal()
				.TextEnd()
				.TextTop()
				.TextCenterVertical()
				.TextBottom()
				.TextCenter());
		}

		class DerivedFromEditor : Editor { }
	}

	class RightToLeftCustomTextAlignmentControl : ITextAlignment
	{
		public TextAlignment HorizontalTextAlignment { get; set; } = TextAlignment.Center;

		public TextAlignment VerticalTextAlignment { get; set; } = TextAlignment.Center;
	}
}

namespace CommunityToolkit.Maui.UnitTests.Extensions.TextAlignmentExtensions
{
	public class PublicTextStyleView : View, ICustomTextAlignment
	{
		public TextAlignment HorizontalTextAlignment { get; set; }

		public TextAlignment VerticalTextAlignment { get; set; }
	}

	class InternalTextStyleView : View, ICustomTextAlignment
	{
		public TextAlignment HorizontalTextAlignment { get; set; }

		public TextAlignment VerticalTextAlignment { get; set; }
	}

	// Ensures custom ITextAlignment interfaces are supported
	interface ICustomTextAlignment : ITextAlignment
	{

	}

	public interface ISomeInterface
	{

	}

	public class ClassConstraintWithInterface : ISomeInterface
	{

	}

	public class ClassConstraint
	{

	}

	class MyGenericPicker<T> : Picker
	{

	}

	public record RecordClassContstraint
	{

	}


	public readonly record struct RecordStructContstraint
	{

	}

	class MoreGenericPicker<T> : MyGenericPicker<T>
	{

	}

	public struct StructConstraint
	{

	}

	public class GenericPicker<TA, TB, TC, TD, TE, TF, TG, TH, TI, TJ, TK, TL, TM> : View, ITextAlignment
		where TA : notnull, ISomeInterface
		where TB : class
		where TC : struct
		where TD : class, ISomeInterface, new()
		//TE has no constraints 
		where TF : notnull
		where TG : unmanaged
		where TH : ISomeInterface?
		where TI : class?
		where TJ : ISomeInterface
		where TK : new()
		where TL : class
		where TM : struct
	{
		public TextAlignment HorizontalTextAlignment { get; set; }

		public TextAlignment VerticalTextAlignment { get; set; }
	}

	class BrandNewControl : View, ITextAlignment
	{
		public TextAlignment HorizontalTextAlignment { get; set; }

		public TextAlignment VerticalTextAlignment { get; set; }
	}
}