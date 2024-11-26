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
			Assert.That(new PublicTextAlignmentView()
							.TextStart()
							.TextCenterHorizontal()
							.TextEnd()
							.TextTop()
							.TextCenterVertical()
							.TextBottom()
							.TextCenter(),
						Is.InstanceOf<PublicTextAlignmentView>());
		}

		[Test]
		public void InternalTextAlignmentView()
		{
			Assert.That(new InternalTextAlignmentView()
							.TextStart()
							.TextCenterHorizontal()
							.TextEnd()
							.TextTop()
							.TextCenterVertical()
							.TextBottom()
							.TextCenter(),
						Is.InstanceOf<InternalTextAlignmentView>());
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
				RecordClassConstraint,
				RecordClassConstraint[],
				RecordStructConstraint>()
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
									RecordClassConstraint,
									RecordClassConstraint[],
									RecordStructConstraint>,
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
									RecordClassConstraint,
									RecordClassConstraint[],
									RecordStructConstraint>();

			Assert.That(textAlignmentView.HorizontalTextAlignment, Is.EqualTo(TextAlignment.Center));

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
									RecordClassConstraint,
									RecordClassConstraint[],
									RecordStructConstraint>,
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
									RecordClassConstraint,
									RecordClassConstraint[],
									RecordStructConstraint>();

			Assert.That(textAlignmentView.HorizontalTextAlignment, Is.EqualTo(TextAlignment.End));
		}

		[Test]
		public void GenericPickerShouldUseThePickerExtension()
		{
			var genericPicker = new MyGenericPicker<string>();

			Assert.That(genericPicker.HorizontalTextAlignment, Is.EqualTo(TextAlignment.Start));

			var generatedPicker = genericPicker.TextEnd();

			Assert.Multiple(() =>
			{
				Assert.That(genericPicker.HorizontalTextAlignment, Is.EqualTo(TextAlignment.End));
				Assert.That(generatedPicker, Is.InstanceOf<MyGenericPicker<string>>());
			});
		}

		[Test]
		public void MoreGenericPickerShouldUseThePickerExtension()
		{
			var genericPicker = new MoreGenericPicker<string>();

			Assert.That(genericPicker.HorizontalTextAlignment, Is.EqualTo(TextAlignment.Start));

			genericPicker.TextEnd();

			Assert.That(genericPicker.HorizontalTextAlignment, Is.EqualTo(TextAlignment.End));
		}

		[Test]
		public void BrandNewControlShouldHaveHisOwnExtensionMethod()
		{
			var brandNewControl = new BrandNewControl();
			Assert.That(brandNewControl.HorizontalTextAlignment, Is.EqualTo(TextAlignment.Start));

			brandNewControl.TextEnd();

			Assert.That(brandNewControl.HorizontalTextAlignment, Is.EqualTo(TextAlignment.End));
		}

		[Test]
		public void SupportDerivedFromBindable()
		{
			Assert.That(new DerivedFromSearchBar()
							.TextStart()
							.TextCenterHorizontal()
							.TextEnd()
							.TextTop()
							.TextCenterVertical()
							.TextBottom()
							.TextCenter()
							.FontSize(8.0)
							.Bold()
							.Italic(),
						Is.InstanceOf<DerivedFromSearchBar>());
		}

		[Test]
		public void SupportCustomTextAlignment()
		{
			Assert.That(new CustomTextAlignmentControl()
							.TextStart()
							.TextCenterHorizontal()
							.TextEnd()
							.TextTop()
							.TextCenterVertical()
							.TextBottom()
							.TextCenter(),
						Is.InstanceOf<CustomTextAlignmentControl>());
		}

		[Test]
		public void AccessModifierForMauiControlsShouldNotBePublic()
		{
			foreach (var (generatedType, control) in GetGeneratedTextAlignmentExtensionTypes())
			{
				if (control.Assembly == typeof(Button).Assembly)
				{
					Assert.That(generatedType.IsPublic, Is.False);
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
					Assert.That(control.IsPublic, Is.EqualTo(generatedType.IsPublic));
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
		string Type { get; set; }
	}
}

namespace CommunityToolkit.Maui.Markup.UnitTests
{

	[TestFixture]
	class LeftToRightTextAlignmentExtensionsTests : BaseMarkupTestFixture<Picker>
	{
		[Test]
		public void TextLeft()
		{
			Bindable.TextEnd();
			Assert.That(Bindable.HorizontalTextAlignment, Is.EqualTo(TextAlignment.End));

			LeftToRight.TextAlignmentExtensions_Picker.TextLeft(Bindable);
			Assert.That(Bindable.HorizontalTextAlignment, Is.EqualTo(TextAlignment.Start));
		}

		[Test]
		public void TextRight()
		{
			Assert.That(Bindable.HorizontalTextAlignment, Is.EqualTo(TextAlignment.Start));

			LeftToRight.TextAlignmentExtensions_Picker.TextRight(Bindable);
			Assert.That(Bindable.HorizontalTextAlignment, Is.EqualTo(TextAlignment.End));
		}

		[Test]
		public void SupportDerivedFromBindable()
		{
			Assert.That(new DerivedFromEntry()
							.TextStart()
							.TextCenterHorizontal()
							.TextEnd()
							.TextTop()
							.TextCenterVertical()
							.TextBottom()
							.TextCenter()
							.FontSize(8.0)
							.Bold()
							.Italic(),
						Is.InstanceOf<DerivedFromEntry>());
		}

		[Test]
		public void SupportCustomTextAlignment()
		{
			Assert.That(new LeftToRightCustomTextAlignmentControl()
						.TextStart()
						.TextCenterHorizontal()
						.TextEnd()
						.TextTop()
						.TextCenterVertical()
						.TextBottom()
						.TextCenter(),
						Is.InstanceOf<LeftToRightCustomTextAlignmentControl>());
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

	[TestFixture]
	class RightToLeftTextAlignmentExtensionsTests : BaseMarkupTestFixture<Picker>
	{

		[Test]
		public void TextRight()
		{
			Bindable.TextEnd();
			Assert.That(Bindable.HorizontalTextAlignment, Is.EqualTo(TextAlignment.End));

			RightToLeft.TextAlignmentExtensions_Picker.TextRight(Bindable);
			Assert.That(Bindable.HorizontalTextAlignment, Is.EqualTo(TextAlignment.Start));
		}

		[Test]
		public void TextLeft()
		{
			Assert.That(Bindable.HorizontalTextAlignment, Is.EqualTo(TextAlignment.Start));

			RightToLeft.TextAlignmentExtensions_Picker.TextLeft(Bindable);
			Assert.That(Bindable.HorizontalTextAlignment, Is.EqualTo(TextAlignment.End));
		}

		[Test]
		public void SupportDerivedFromBindable()
		{
			Assert.That(new DerivedFromEditor()
							.TextStart()
							.TextCenterHorizontal()
							.TextEnd()
							.TextTop()
							.TextCenterVertical()
							.TextBottom()
							.TextCenter()
							.FontSize(8.0)
							.Bold()
							.Italic(),
						Is.InstanceOf<DerivedFromEditor>());
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

			Assert.Multiple(() =>
			{
				Assert.That(partialClassControl.IsPartial, Is.True);
				Assert.That(partialClassControl, Is.InstanceOf<PartialClassControl>());
			});
		}

		[Test]
		public void SupportCustomTextAlignment()
		{
			Assert.That(new RightToLeftCustomTextAlignmentControl()
							.TextStart()
							.TextCenterHorizontal()
							.TextEnd()
							.TextTop()
							.TextCenterVertical()
							.TextBottom()
							.TextCenter(),
						Is.InstanceOf<RightToLeftCustomTextAlignmentControl>());
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

	public record RecordClassConstraint
	{

	}


	public readonly record struct RecordStructConstraint
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