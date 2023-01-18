using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace CommunityToolkit.Maui.Markup.SourceGenerators;

static class SourceStringExtensions
{
	public static void FormatText(ref string classSource, CSharpParseOptions? options = null)
	{
		var source = CSharpSyntaxTree.ParseText(SourceText.From(classSource, Encoding.UTF8), options);
		var formattedRoot = (CSharpSyntaxNode)source.GetRoot().NormalizeWhitespace();

		classSource = CSharpSyntaxTree.Create(formattedRoot).ToString();
	}
}