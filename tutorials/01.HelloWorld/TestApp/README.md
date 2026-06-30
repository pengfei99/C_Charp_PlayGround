# C# tutorial 01

## Setup your first C# .NET project

```powershell
# ensure you have the .NET SDK installed
dotnet --version

# create a .NET project skeleton
dotnet new
```

you should see the below output

```text
The 'dotnet new' command creates a .NET project based on a template.

Common templates are:
Template Name        Short Name  Language    Tags               
-------------------  ----------  ----------  -----------------------
Blazor Web App       blazor      [C#]        Web/Blazor/WebAssembly 
Class Library        classlib    [C#],F#,VB  Common/Library     
Console App          console     [C#],F#,VB  Common/Console     
MSTest Test Project  mstest      [C#],F#,VB  Test/MSTest/Desktop/Web
Windows Forms App    winforms    [C#],VB     Common/WinForms    
WPF Application      wpf         [C#],VB     Common/WPF         

An example would be:
   dotnet new console

Display template options with:
   dotnet new console -h
Display all installed templates with:
   dotnet new list
Display templates available on NuGet.org with:
   dotnet new search web
```

In this tutorial, we will create a console App, so we will run the below command

```powershell
# create a console app with the project name `TestApp`
dotnet new console -o TestApp
```

## Content of the project skeleton

In the generated project skeleton, you should see the below contents


- bin: folder which contains the build binary of the application
- obj: 
- Program.cs: is the entry point of your application, like `main.py`. 
- TestApp.csproj


### The `program.cs` 

Modern .NET uses Top-Level Statements, meaning we don't need boilerplate class Program or static void Main structures.

### The `.csproj` file (C# Project file)

The `.csproj` file is the manager of your specific project. It is the C# equivalent of Python's `requirements.txt / pyproject.toml`. It does even more. It tells the .NET compiler exactly how to build, package, and run your application.

Below is an `.csproj` file generated with dotnet 10.
```cs
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>

```

`<Project Sdk="Microsoft.NET.Sdk">` tells .NET what kind of project this is. The `Sdk` attribute brings in a predefined set of build rules.

- `Microsoft.NET.Sdk` is used for Console apps, Class Libraries, and backend logic.
- If you build a web app, you will see `Microsoft.NET.Sdk.Web`.
- If you build a desktop app (like WPF), you will see `Microsoft.NET.Sdk.WindowsDesktop`.

The `<PropertyGroup> Block` holds configuration settings for your project.

- `<OutputType>Exe</OutputType>`: This tells the compiler to generate an executable file (.exe on Windows) that can be run directly. If this line is missing or says Library, it compiles into a .dll file instead (meant to be referenced by other apps).

- `<TargetFramework>net8.0</TargetFramework>`: This specifies the version of the .NET runtime your app requires. It defines which library features and C# language capabilities are available to you.

- `<ImplicitUsings>enable</ImplicitUsings>`: It tells the compiler to automatically include common using statements (like System, System.Linq, System.Collections.Generic) globally across your files so you don't have to type them at the top of every single C# file.

- `<Nullable>enable</Nullable>`: This turns on C#'s powerful Nullable Reference Types feature. It forces the compiler to warn you if a variable might accidentally result in a NullReferenceException (the dreaded "Object reference not set to an instance of an object" crash).