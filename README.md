# RazorString
Quickly render Razor components to strings and TextWriters.

## Quickstart

### 1.  Install RazorString

	Install-Package RazorString

### 2.  Create a project that will contain your templates.
It should start out looking something like this:

```xml
<Project Sdk="Microsoft.NET.Sdk.Razor">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.Web" Version="8.0.3" />
  </ItemGroup>

</Project>

```

The important things are that:
* Is is a ```Microsoft.NET.Sdk.Razor``` project
* It references ```Microsoft.AspNetCore.Components.Web```

### 3.  Create some Templates
Get creative!  You can keep things simple or complex.

Here's a fun example.

Create a file named ```Composite.razor```:
```csharp
Hello @Context.Name!  I see that you were born on @Context.Date_Of_Birth.

Today we will eat:
Breakfast: @Breakfast
Lunch:     @Lunch
Dinner:    @Dinner

@code {
    [Parameter]
    public required CompositeContext Context { get; init; }

    [Parameter]
    public required string Breakfast { get; init; }

    [Parameter]
    public required string Lunch { get; init; }

    [Parameter]
    public required string Dinner { get; init; }

}
```

Then also create a file named ```CompositeContext.cs```:
```csharp
namespace Example.Templates {
    public record CompositeContext {
        public required string Name { get; init; }
        public required DateTime Date_Of_Birth { get; init; }
    }
}
```

### 4.  Put your template to use!
```csharp
var Template = TemplateFactory.Default.FromComponent<Composite>()
	.WithParameters([
		("Breakfast", "Coffee"),
		("Lunch", "Sandwich"),
		("Dinner", "Pizza"),
		("Context", new CompositeContext() {
			Name = "John Smith",
			Date_Of_Birth = new DateTime(1901, 01, 01),
		})
	]);

var Content = await Template.RenderAsync();

Console.WriteLine(Content);
```

## Advanced Usage

### Strongly-Named Arguments
If your Razor templates have a property named ```Context```:
```csharp
@code {
    [Parameter]
    public required CompositeContext Context { get; init; }
}
```

You can pass in that argument with:
```csharp
var Template = TemplateFactory.Default.FromComponent<Composite>()
	.WithContext(new CompositeContext() {
		Name = "John Smith",
		Date_Of_Birth = new DateTime(1901, 01, 01),
	});

```


### Reusable Templates
If you're going to use the same template in a loop, reuse it:
```csharp
var Template = TemplateFactory.Default.FromComponent<Composite>();

foreach(var item in DataSet){
	var Derived = Template
		.WithParameters([
			("Breakfast", item.Breakfast),
			("Lunch", item.Lunch),
			("Dinner", item.Dinner),
			("Context", new CompositeContext() {
				Name = item.Name,
				Date_Of_Birth = item.DoB,
			}),
		]);

	var Content = await Derived.RenderAsync();

	Console.WriteLine(Content);
}

```

### Composable Templates
You don't have to provide all the args at once and you can override values.
```csharp

//Load our base template with no args
var EmptyTemplate = TemplateFactory.Default.FromComponent<Composite>();

//Set some values
var TemplateWithDefaults = EmptyTemplate
	.WithParameters([
		("Context", new CompositeContext() {
			Name = "John Smith",
			Date_Of_Birth = new DateTime(1901, 01, 01),
		}),
		("Breakfast", "Coffee"),
	]);

foreach(var Meal in Meals){
	var MealPlan = TemplateWithDefaults
		.WithParameters([
			("Breakfast", Meal.Breakfast), //Override the default 
			("Lunch", Meal.Lunch),
			("Dinner", Meal.Dinner),
		]);

	var Content = await MealPlan.RenderAsync();

	Console.WriteLine(Content);

}


```