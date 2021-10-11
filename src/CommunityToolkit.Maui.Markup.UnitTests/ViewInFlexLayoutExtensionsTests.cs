using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using Xunit;

namespace CommunityToolkit.Maui.Markup.UnitTests
{
	
	public class ViewInFlexLayoutExtensionsTests : MarkupBaseTest<BoxView>
	{
		[Fact]
		public void AlignSelf()
		{
			FlexLayout.SetAlignSelf(Bindable, FlexAlignSelf.End);
			Bindable?.AlignSelf(FlexAlignSelf.Start);

			Assert.That(FlexLayout.GetAlignSelf(Bindable), Is.EqualTo(FlexAlignSelf.Start));
		}

		[Fact]
		public void Basis()
		{
			FlexLayout.SetBasis(Bindable, FlexBasis.Auto);
			Bindable?.Basis(50);

			Assert.That(FlexLayout.GetBasis(Bindable), Is.EqualTo(new FlexBasis(50)));
		}

		[Fact]
		public void Grow()
		{
			FlexLayout.SetGrow(Bindable, 0f);
			Bindable?.Grow(1f);

			Assert.That(FlexLayout.GetGrow(Bindable), Is.EqualTo(1f));
		}

		[Fact]
		public void Order()
		{
			FlexLayout.SetOrder(Bindable, 0);
			Bindable?.Order(1);

			Assert.That(FlexLayout.GetOrder(Bindable), Is.EqualTo(1));
		}

		[Fact]
		public void Shrink()
		{
			FlexLayout.SetShrink(Bindable, 1f);
			Bindable?.Shrink(0f);

			Assert.That(FlexLayout.GetShrink(Bindable), Is.EqualTo(0f));
		}
	}
}