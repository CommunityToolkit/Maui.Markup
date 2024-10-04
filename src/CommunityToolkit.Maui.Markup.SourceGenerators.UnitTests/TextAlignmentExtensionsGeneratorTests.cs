﻿using System.Runtime.InteropServices;
using NUnit.Framework;
using static CommunityToolkit.Maui.Markup.SourceGenerators.UnitTests.CSharpSourceGeneratorVerifier<CommunityToolkit.Maui.Markup.SourceGenerators.TextAlignmentExtensionsGenerator>;

namespace CommunityToolkit.Maui.Markup.SourceGenerators.UnitTests;

public class TextAlignmentExtensionsGeneratorTests
{
	static readonly string assemblyVersion = typeof(TextAlignmentExtensionsGenerator).Assembly.GetName().Version?.ToString() ?? throw new InvalidOleVariantTypeException("Assembly name cannot be null");
	static readonly string textAlignmentExtensionsGeneratorFullName = typeof(TextAlignmentExtensionsGenerator).Assembly.FullName ?? throw new InvalidOleVariantTypeException("Assembly fullname cannot be null");

	[Test]
	public async Task VerifyGeneratedSource_WhenClassImplementsITextAlignmentInterface()
	{
		// Arrange
		const string source = /* language=C#-test */ """
using Microsoft.Maui;
namespace MyNamespace;

public class MyClass : ITextAlignment
{
	public TextAlignment HorizontalTextAlignment { get; set; } = TextAlignment.Center;
	public TextAlignment VerticalTextAlignment { get; set; } = TextAlignment.Center;
}
""";

		// Act // Assert
		await VerifySourceGeneratorAsync(
			source,
			"MyClassTextAlignmentExtensions.g.cs",
			GenerateSourceCode(textAlignmentExtensionsGeneratorFullName,
				new("MyClass", "public", "MyNamespace", string.Empty, string.Empty), string.Empty, string.Empty),
			[]);
	}

	[Test]
	public async Task VerifyGeneratedSource_WhenClassIsGeneric()
	{
		// Arrange
		const string source = /* language=C#-test */ """
using System;
using Microsoft.Maui;
namespace MyNamespace;

public class MyClass<T, U> : Microsoft.Maui.ITextAlignment
						  where T : IDisposable, new()
						  where U : class
{
	public TextAlignment HorizontalTextAlignment { get; set; } = TextAlignment.Center;
	public TextAlignment VerticalTextAlignment { get; set; } = TextAlignment.Center;
}
""";

		// Act // Assert
		await VerifySourceGeneratorAsync(
			source,
			"MyClassTextAlignmentExtensions.g.cs",
			GenerateSourceCode(textAlignmentExtensionsGeneratorFullName,
				new("MyClass", "public", "MyNamespace", "<T, U>", "where T : IDisposable, new() where U : class"), string.Empty, string.Empty),
			[]);
	}

	static string GenerateSourceCode(string fullClassName, TextAlignmentClassMetadata textAlignmentClassMetadata, string genericArguments, string genericTypeParameters) =>
		/* language=C#-test */ $$"""
// <auto-generated>
// See: CommunityToolkit.Maui.Markup.SourceGenerators.TextAlignmentGenerator

#nullable enable
#pragma warning disable

using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace CommunityToolkit.Maui.Markup
{
   /// <summary>
   /// Extension Methods for <see cref="ITextAlignment"/>
   /// </summary>
   [global::System.CodeDom.Compiler.GeneratedCode("{{fullClassName}}", "{{assemblyVersion}}")]
   [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
   {{textAlignmentClassMetadata.ClassAccessModifier}} static partial class TextAlignmentExtensions_{{textAlignmentClassMetadata.ClassName}}
   {
       /// <summary>
       /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Start"/>
       /// </summary>
       /// <param name="textAlignmentControl"></param>
       /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.Start"/></returns>
       public static TAssignable TextStart{{genericTypeParameters}}(this TAssignable textAlignmentControl)
		where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
       {
           ArgumentNullException.ThrowIfNull(textAlignmentControl);

           if (textAlignmentControl is not ITextAlignment)
           {
               throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
           }

           textAlignmentControl.HorizontalTextAlignment = TextAlignment.Start;
           return textAlignmentControl;
       }

       /// <summary>
       /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Center"/>
       /// </summary>
       /// <param name="textAlignmentControl"></param>
       /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.Center"/></returns>
       public static TAssignable TextCenterHorizontal{{genericTypeParameters}}(this TAssignable textAlignmentControl)
		where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
       {
           ArgumentNullException.ThrowIfNull(textAlignmentControl);

           if (textAlignmentControl is not ITextAlignment)
           {
               throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
           }

           textAlignmentControl.HorizontalTextAlignment = TextAlignment.Center;
           return textAlignmentControl;
       }

       /// <summary>
       /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.End"/>
       /// </summary>
       /// <param name="textAlignmentControl"></param>
       /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.End"/></returns>
       public static TAssignable TextEnd{{genericTypeParameters}}(this TAssignable textAlignmentControl)
		where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
       {
           ArgumentNullException.ThrowIfNull(textAlignmentControl);

           if (textAlignmentControl is not ITextAlignment)
           {
               throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
           }

           textAlignmentControl.HorizontalTextAlignment = TextAlignment.End;
           return textAlignmentControl;
       }

       /// <summary>
       /// <see cref="ITextAlignment.VerticalTextAlignment"/> = <see cref="TextAlignment.Start"/>
       /// </summary>
       /// <param name="textAlignmentControl"></param>
       /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.Start"/></returns>
       public static TAssignable TextTop{{genericTypeParameters}}(this TAssignable textAlignmentControl)
		where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
       {
           ArgumentNullException.ThrowIfNull(textAlignmentControl);

           if (textAlignmentControl is not ITextAlignment)
           {
               throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
           }

           textAlignmentControl.VerticalTextAlignment = TextAlignment.Start;
           return textAlignmentControl;
       }

       /// <summary>
       /// <see cref="ITextAlignment.VerticalTextAlignment"/> = <see cref="TextAlignment.Center"/>
       /// </summary>
       /// <param name="textAlignmentControl"></param>
       /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.Center"/></returns>
       public static TAssignable TextCenterVertical{{genericTypeParameters}}(this TAssignable textAlignmentControl)
		where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
       {
           ArgumentNullException.ThrowIfNull(textAlignmentControl);

           if (textAlignmentControl is not ITextAlignment)
           {
               throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
           }

           textAlignmentControl.VerticalTextAlignment = TextAlignment.Center;
           return textAlignmentControl;
       }

       /// <summary>
       /// <see cref="ITextAlignment.VerticalTextAlignment"/> = <see cref="TextAlignment.End"/>
       /// </summary>
       /// <param name="textAlignmentControl"></param>
       /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.End"/></returns>
       public static TAssignable TextBottom{{genericTypeParameters}}(this TAssignable textAlignmentControl)
		where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
       {
           ArgumentNullException.ThrowIfNull(textAlignmentControl);

           if (textAlignmentControl is not ITextAlignment)
           {
               throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
           }

           textAlignmentControl.VerticalTextAlignment = TextAlignment.End;
           return textAlignmentControl;
       }

       /// <summary>
       /// <see cref="ITextAlignment.VerticalTextAlignment"/> = <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Center"/>
       /// </summary>
       /// <param name="textAlignmentControl"></param>
       /// <returns><typeparamref name="TAssignable"/> with added <see cref="TextAlignment.Center"/></returns>
       public static TAssignable TextCenter{{genericTypeParameters}}(this TAssignable textAlignmentControl)
		where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
           => textAlignmentControl.TextCenterHorizontal{{genericTypeParameters}}().TextCenterVertical{{genericTypeParameters}}();
   }


   // The extensions in these sub-namespaces are designed to be used together with the extensions in the parent namespace.
   // Keep them in a single file for better maintainability

   namespace LeftToRight
   {
       /// <summary>
       /// Extension Methods for <see cref="ITextAlignment"/>
       /// </summary>
       [global::System.CodeDom.Compiler.GeneratedCode("{{fullClassName}}", "{{assemblyVersion}}")]
       [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
       {{textAlignmentClassMetadata.ClassAccessModifier}} static partial class TextAlignmentExtensions_{{textAlignmentClassMetadata.ClassName}}
       {
           /// <summary>
           /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Start"/>
           /// </summary>
           /// <param name="textAlignmentControl"></param>
           /// <returns><typeparamref name="TAssignable"/> with <see cref="TextAlignment.Start"/></returns>
           public static TAssignable TextLeft{{genericTypeParameters}}(this TAssignable textAlignmentControl)
			where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
           {
               ArgumentNullException.ThrowIfNull(textAlignmentControl);

               if (textAlignmentControl is not ITextAlignment)
               {
                   throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
               }

               textAlignmentControl.HorizontalTextAlignment = TextAlignment.Start;
               return textAlignmentControl;
           }

           /// <summary>
           /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.End"/>
           /// </summary>
           /// <param name="textAlignmentControl"></param>
           /// <returns><typeparamref name="TAssignable"/> with <see cref="TextAlignment.End"/></returns>
           public static TAssignable TextRight{{genericTypeParameters}}(this TAssignable textAlignmentControl) where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
           {
               ArgumentNullException.ThrowIfNull(textAlignmentControl);

               if (textAlignmentControl is not ITextAlignment)
               {
                    throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
               }

               textAlignmentControl.HorizontalTextAlignment = TextAlignment.End;
               return textAlignmentControl;
           }
       }
   }

   // The extensions in these sub-namespaces are designed to be used together with the extensions in the parent namespace.
   // Keep them in a single file for better maintainability
   namespace RightToLeft
   {
       /// <summary>
       /// Extension methods for <see cref="ITextAlignment"/>
       /// </summary>
       {{textAlignmentClassMetadata.ClassAccessModifier}} static partial class TextAlignmentExtensions_{{textAlignmentClassMetadata.ClassName}}
       {
           /// <summary>
           /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.End"/>
           /// </summary>
           /// <param name="textAlignmentControl"></param>
           /// <returns><typeparamref name="TAssignable"/> with <see cref="TextAlignment.End"/></returns>
           public static TAssignable TextLeft{{genericTypeParameters}}(this TAssignable textAlignmentControl)
			where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
           {
               ArgumentNullException.ThrowIfNull(textAlignmentControl);

               if (textAlignmentControl is not ITextAlignment)
               {
                   throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
               }

               textAlignmentControl.HorizontalTextAlignment = TextAlignment.End;
               return textAlignmentControl;
           }

           /// <summary>
           /// <see cref="ITextAlignment.HorizontalTextAlignment"/> = <see cref="TextAlignment.Start"/>
           /// </summary>
           /// <param name="textAlignmentControl"></param>
           /// <returns><typeparamref name="TAssignable"/> with <see cref="TextAlignment.Start"/></returns>
           public static TAssignable TextRight{{genericTypeParameters}}(this TAssignable textAlignmentControl)
			where TAssignable : {{textAlignmentClassMetadata.Namespace}}.{{textAlignmentClassMetadata.ClassName}}{{genericArguments}}{{textAlignmentClassMetadata.GenericConstraints}}
           {
               ArgumentNullException.ThrowIfNull(textAlignmentControl);

               if (textAlignmentControl is not ITextAlignment)
               {
                   throw new ArgumentException($"Element must implement {nameof(ITextAlignment)}", nameof(textAlignmentControl));
               }

               textAlignmentControl.HorizontalTextAlignment = TextAlignment.Start;
               return textAlignmentControl;
           }
       }
   }
}
""";
}