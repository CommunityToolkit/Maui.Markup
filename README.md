<img src="https://user-images.githubusercontent.com/13558917/137551073-ac8958bf-83e3-4ae3-8623-4db6dce49d02.png" alt=".NET MAUI Community Toolkit bot" width=125 />  [<img src="https://raw.githubusercontent.com/dotnet-foundation/swag/master/logo/dotnetfoundation_v4.svg" alt=".NET Foundation" width=100>](https://dotnetfoundation.org) 

[![Build Status](https://dev.azure.com/dotnet/CommunityToolkit/_apis/build/status/CommunityToolkit.Maui.Markup?branchName=main)](https://dev.azure.com/dotnet/CommunityToolkit/_build/latest?definitionId=173&branchName=main) [![NuGet Version](https://img.shields.io/nuget/vpre/CommunityToolkit.Maui.Markup)](https://www.nuget.org/packages/CommunityToolkit.Maui.Markup) [![NuGet Downloads](https://img.shields.io/nuget/dt/CommunityToolkit.Maui.Markup)](https://www.nuget.org/packages/CommunityToolkit.Maui.Markup)


# .NET MAUI Markup Community Toolkit

The .NET MAUI Markup Community Toolkit is a collection of Fluent C# Extension Methods that allows developers to continue architecting their apps using MVVM, Bindings, Resource Dictionaries, etc., without the need for XAML

All features are contributed by you, our amazing .NET community, and maintained by a core set of maintainers.

And – the best part – the features you add to the .NET MAUI Toolkit may one day be included into the official .NET MAUI library! We leverage the Community Toolkits to debut new features and work closely with the .NET MAUI engineering team to nominate features for promotion.

## Getting Started

In order to use the .NET MAUI Community Toolkit you need to call the extension method in your `MauiProgram.cs` file as follows:

```csharp
using CommunityToolkit.Maui.Markup;

public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();
    // Initialise the toolkit
    builder.UseMauiApp<App>().UseMauiCommunityToolkitMarkup();
    // the rest of your logic...
}
```

## Documentation

<a href="https://learn.microsoft.com/en-us/dotnet/communitytoolkit/maui/markup/markup"><img width="200" alt="image" src="https://user-images.githubusercontent.com/13558917/232885041-35b62d65-26d3-44a7-a525-5239ac811498.png"></a>

All of the documentation for `CommunityToolkit.Maui.Markup` can be found here on [Microsoft Learn](https://learn.microsoft.com/dotnet/communitytoolkit/maui/markup/markup):

https://learn.microsoft.com/dotnet/communitytoolkit/maui/markup/markup


## Examples

Here are some brief examples showing how common tasks can be achieved through the use of the Markup package.

### Bindings

C# Markup allows us to define the binding fluently and therefore chain multiple methods together to reduce the verbosity of our code:

```csharp
new Entry()
    .Bind(Entry.TextProperty, static (ViewModel vm) => vm.RegistrationCode);
```

For further details on the possible options for the `Bind` method refer to the [`BindableObject` extensions documentation](https://docs.microsoft.com/dotnet/communitytoolkit/maui/markup/extensions/bindable-object-extensions).

### Sizing

Markup allows us to define the sizing fluently and therefore chain multiple methods together to reduce the verbosity of our code:

```csharp
new Entry().Size(200, 40);
```

For further details on the possible options for the `Size` method refer to the [`VisualElement` extensions documentation](https://docs.microsoft.com/dotnet/communitytoolkit/maui/markup/extensions/visual-element-extensions).

### In-depth example

This example creates a `Grid` object, with child `Label` and `Entry` objects. The `Label` displays text, and the `Entry` data binds to the `RegistrationCode` property of the viewmodel. Each child view is set to appear in a specific row in the `Grid`, and the `Entry` spans all the columns in the `Grid`. In addition, the height of the `Entry` is set, along with its keyboard, colors, the font size of its text, and its `Margin`. 

C# Markup extensions also allow developers to define names for Columns and Rows (e.g. `Column.Input`) using an `enum`.

C# Markup enables this to be defined using its fluent API:

```csharp
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

class SampleContentPage : ContentPage
{
    public SampleContentPage()
    {
        Content = new Grid
        {
            RowDefinitions = Rows.Define(
                (Row.TextEntry, 36)),

            ColumnDefinitions = Columns.Define(
                (Column.Description, Star),
                (Column.Input, Stars(2))),

            Children =
            {
                new Label()
                    .Text("Code:")
                    .Row(Row.TextEntry).Column(Column.Description),

                new Entry
                {
                    Keyboard = Keyboard.Numeric,
                    BackgroundColor = Colors.AliceBlue,
                }.Row(Row.TextEntry).Column(Column.Input)
                 .FontSize(15)
                 .Placeholder("Enter number")
                 .TextColor(Colors.Black)
                 .Height(44)
                 .Margin(5, 5)
                 .Bind(Entry.TextProperty, static (ViewModel vm) vm => vm.RegistrationCode)
            }
        };
    }

    enum Row { TextEntry }
    enum Column { Description, Input }
}
```


## Submitting A New Feature

New features will follow this workflow, described in more detail in the steps below

[![New Feature Workflow](https://user-images.githubusercontent.com/13558917/160910778-1e61f478-f1f6-48b4-8d37-8016eae1bd12.png)](./build/workflow.sketch)

### 1. Discussion Started

Debate pertaining to new Maui Toolkit features takes place in the form of [Discussions](https://github.com/communitytoolkit/maui.markup/discussions) in this repo.

If you want to suggest a feature, discuss current design notes or proposals, etc., please [open a new Discussion topic](https://github.com/communitytoolkit/maui.markup/discussions/new).

Discussions that are short and stay on topic are much more likely to be read. If you leave comment number fifty, chances are that only a few people will read it. To make discussions easier to navigate and benefit from, please observe a few rules of thumb:

- Discussion should be relevant to the .NET MAUI Toolkit. If they are not, they will be summarily closed.
- Choose a descriptive topic that clearly communicates the scope of discussion.
- Stick to the topic of the discussion. If a comment is tangential, or goes into detail on a subtopic, start a new discussion and link back.
- Is your comment useful for others to read, or can it be adequately expressed with an emoji reaction to an existing comment?

### 2. Proposal Submitted
Once you have a fully fleshed out proposal describing a new feature in syntactic and semantic detail, please [open an issue for it](https://github.com/communitytoolkit/maui.markup/issues/new/choose), and it will be labeled as a [Proposal](https://github.com/communitytoolkit/maui.markup/issues?q=is%3Aopen+is%3Aissue+label%3Aproposal). The comment thread on the issue can be used to hash out or briefly discuss details of the proposal, as well as pros and cons of adopting it into the .NET MAUI Toolkit. If an issue does not meet the bar of being a full proposal, we may move it to a discussion, so that it can be further matured. Specific open issues or more expansive discussion with a proposal will often warrant opening a side discussion rather than cluttering the comment section on the issue.

### 3. Proposal Championed
When a member of the .NET MAUI Toolkit core team finds that a proposal merits promotion into the Toolkit, they can [Champion](https://github.com/communitytoolkit/maui.markup/issues?q=is%3Aopen+is%3Aissue+label%3A%22proposal+champion%22) it, which means that they will bring it to the monthly [.NET MAUI Toolkit Community Standup](https://www.youtube.com/watch?v=0ZBh2Hl54ZY). 

### 4. Proposal Approved
The .NET MAUI Toolkit core team will collectively vote to work on adopting and/or modifying the proposal, requiring a majority approval (i.e. greater than 50%) to be added to the Toolkit.

Once a Proposal has been championed and has received a majority approval from the .NET MAUI Toolkit core team, a Pull Request can be opened.

### 5. Pull Request Approved
After a Pull Request has been submitted, it will be reviewed and approved by the Proposal Champion. 

Every new feature also requires an associated sample to be added to the .NET MAUI Toolkit Sample app.

### 6. Documentation Complete 
Before a Pull Request can be merged into the .NET MAUI Toolkit, the Pull Request Author must also submit the documentation to our [documentation repository](https://github.com/MicrosoftDocs/CommunityToolkit).

### 7. Merged
Once a Pull Request has been reviewed + approved AND the documentation has been written, submitted and approved, the new feature will be merged adding it to the .NET MAUI Toolkit

## Code of Conduct
As a part of the .NET Foundation, we have adopted the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct). Please familiarize yourself with that before participating with this repository. Thanks!

## .NET Foundation
This project is supported by the [.NET Foundation](https://dotnetfoundation.org).
