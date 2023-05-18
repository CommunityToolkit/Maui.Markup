using System.Reflection;
using CommunityToolkit.Maui.Markup.UnitTests.Base;
using CommunityToolkit.Maui.UnitTests.Extensions.TextAlignmentExtensions;
using NUnit.Framework;
using Unique.Namespace.To.Test.Interface;

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
		public void PublicTextAlignmentView()
		{
			Assert.IsInstanceOf<PublicTextAlignmentView>(
				new PublicTextAlignmentView()
				.TextStart()
				.TextCenterHorizontal()
				.TextEnd()
				.TextTop()
				.TextCenterVertical()
				.TextBottom()
				.TextCenter());
		}

		[Test]
		public void InternalTextAlignmentView()
		{
			Assert.IsInstanceOf<InternalTextAlignmentView>(
				new InternalTextAlignmentView()
				.TextStart()
				.TextCenterHorizontal()
				.TextEnd()
				.TextTop()
				.TextCenterVertical()
				.TextBottom()
				.TextCenter());
		}

		[Test]
		public void Extensions_For_Generic_Class()
		{
			var textAlignmentView = new GenericPicker<
				ClassConstraintWithInterface,
				ClassConstraint,
				StructConstraint,
				ClassConstraintWithInterface,
				string,
				int,
				bool,
				ClassConstraintWithInterface?,
				ClassConstraint[],
				ClassConstraintWithInterface,
				RecordClassContstraint,
				RecordClassContstraint[],
				RecordStructContstraint>()
					.TextCenter<GenericPicker<ClassConstraintWithInterface,
									ClassConstraint,
									StructConstraint,
									ClassConstraintWithInterface,
									string,
									int,
									bool,
									ClassConstraintWithInterface?,
									ClassConstraint[],
									ClassConstraintWithInterface,
									RecordClassContstraint,
									RecordClassContstraint[],
									RecordStructContstraint>,
									ClassConstraintWithInterface,
									ClassConstraint,
									StructConstraint,
									ClassConstraintWithInterface,
									string,
									int,
									bool,
									ClassConstraintWithInterface?,
									ClassConstraint[],
									ClassConstraintWithInterface,
									RecordClassContstraint,
									RecordClassContstraint[],
									RecordStructContstraint>();

			Assert.AreEqual(TextAlignment.Center, textAlignmentView.HorizontalTextAlignment);

			textAlignmentView.TextEnd<GenericPicker<ClassConstraintWithInterface,
									ClassConstraint,
									StructConstraint,
									ClassConstraintWithInterface,
									string,
									int,
									bool,
									ClassConstraintWithInterface?,
									ClassConstraint[],
									ClassConstraintWithInterface,
									RecordClassContstraint,
									RecordClassContstraint[],
									RecordStructContstraint>,
									ClassConstraintWithInterface,
									ClassConstraint,
									StructConstraint,
									ClassConstraintWithInterface,
									string,
									int,
									bool,
									ClassConstraintWithInterface?,
									ClassConstraint[],
									ClassConstraintWithInterface,
									RecordClassContstraint,
									RecordClassContstraint[],
									RecordStructContstraint>();

			Assert.AreEqual(TextAlignment.End, textAlignmentView.HorizontalTextAlignment);
		}

		[Test]
		public void GenericPickerShouldUseThePickerExtension()
		{
			var genericPicker = new MyGenericPicker<string>();

			Assert.AreEqual(TextAlignment.Start, genericPicker.HorizontalTextAlignment);

			var generatedPicker = genericPicker.TextEnd();

			Assert.AreEqual(TextAlignment.End, genericPicker.HorizontalTextAlignment);
			Assert.IsInstanceOf<MyGenericPicker<string>>(generatedPicker);
		}

		[Test]
		public void MoreGenericPickerShouldUseThePickerExtension()
		{
			var genericPicker = new MoreGenericPicker<string>();

			Assert.AreEqual(TextAlignment.Start, genericPicker.HorizontalTextAlignment);

			genericPicker.TextEnd();

			Assert.AreEqual(TextAlignment.End, genericPicker.HorizontalTextAlignment);
		}

		[Test]
		public void BrandNewControlShouldHaveHisOwnExtensionMethod()
		{
			var brandNewControl = new BrandNewControl();
			Assert.AreEqual(TextAlignment.Start, brandNewControl.HorizontalTextAlignment);

			brandNewControl.TextEnd();

			Assert.AreEqual(TextAlignment.End, brandNewControl.HorizontalTextAlignment);
		}

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

		[Test]
		public void AccessModifierForMauiControlsShouldNotBePublic()
		{
			foreach (var (generatedType, control) in GetGeneratedTextAlignmentExtensionTypes())
			{
				if (control.Assembly == typeof(Button).Assembly)
				{
					Assert.False(generatedType.IsPublic);
				}
			}
		}

		[Test]
		public void AccessModifierForCustomControlsShouldMatchTheControl()
		{
			var executingAssembly = Assembly.GetExecutingAssembly();

			foreach (var (generatedType, control) in GetGeneratedTextAlignmentExtensionTypes())
			{
				if (control.Assembly == executingAssembly)
				{
					Assert.AreEqual(control.IsPublic, generatedType.IsPublic);
				}
			}
		}

		static IEnumerable<(Type generatedType, Type control)> GetGeneratedTextAlignmentExtensionTypes()
		{
			return from type in Assembly.GetExecutingAssembly().GetTypes()
				   where type.Name.StartsWith("TextAlignmentExtensions_")
				   let method = type.GetMethods().Single(m => m.Name.StartsWith("TextLeft") || m.Name.StartsWith("TextStart"))
				   let control = method.GetParameters()[0].ParameterType.BaseType
				   select (type, control);
		}

		class DerivedFromSearchBar : SearchBar { }
	}

	class CustomTextAlignmentControl : ITextAlignment
	{
		public TextAlignment HorizontalTextAlignment { get; set; } = TextAlignment.Center;

		public TextAlignment VerticalTextAlignment { get; set; } = TextAlignment.Center;
	}

	class MyEntry : Entry, IEntry
	{
		public string Type { get; set; } = string.Empty;
	}

	interface IMyEntry
	{
		public string Type { get; set; }
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
		{
			Bindable.TextEnd();
			Assert.AreEqual(TextAlignment.End, Bindable.HorizontalTextAlignment);

			Bindable.TextLeft();
			Assert.AreEqual(TextAlignment.Start, Bindable.HorizontalTextAlignment);
		}

		[Test]
		public void TextRight()
		{
			Assert.AreEqual(TextAlignment.Start, Bindable.HorizontalTextAlignment);

			Bindable.TextRight();
			Assert.AreEqual(TextAlignment.End, Bindable.HorizontalTextAlignment);
		}

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
		public void TextRight()
		{
			Bindable.TextEnd();
			Assert.AreEqual(TextAlignment.End, Bindable.HorizontalTextAlignment);

			Bindable.TextRight();
			Assert.AreEqual(TextAlignment.Start, Bindable.HorizontalTextAlignment);
		}

		[Test]
		public void TextLeft()
		{
			Assert.AreEqual(TextAlignment.Start, Bindable.HorizontalTextAlignment);

			Bindable.TextLeft();
			Assert.AreEqual(TextAlignment.End, Bindable.HorizontalTextAlignment);
		}

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
		public void SupportPartialClasses()
		{
			var partialClassControl = new PartialClassControl()
				.TextStart()
				.TextCenterHorizontal()
				.TextEnd()
				.TextTop()
				.TextCenterVertical()
				.TextBottom()
				.TextCenter();

			Assert.True(partialClassControl.IsPartial);
			Assert.IsInstanceOf<PartialClassControl>(partialClassControl);
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
	public class PublicTextAlignmentView : View, ICustomTextAlignment
	{
		public TextAlignment HorizontalTextAlignment { get; set; }

		public TextAlignment VerticalTextAlignment { get; set; }
	}

	class InternalTextAlignmentView : View, ICustomTextAlignment
	{
		public TextAlignment HorizontalTextAlignment { get; set; }

		public TextAlignment VerticalTextAlignment { get; set; }
	}

	// Ensures custom ITextAlignment interfaces are supported
	interface ICustomTextAlignment : ITextAlignment
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

	partial class PartialClassControl : View, ITextAlignment
	{
		public TextAlignment HorizontalTextAlignment { get; set; }

		public TextAlignment VerticalTextAlignment { get; set; }
	}

	partial class PartialClassControl
	{
		public bool IsPartial { get; } = true;
	}
}

namespace Unique.Namespace.To.Test.Interface
{
	public interface ISomeInterface
	{

	}
}