# Hello world application

In this tutorial, I will create a hello world application with dotnet CLI. You must install dotnet sdk before.

## 1. Check .NET installation

Open PowerShell or Command Prompt and run:

```shell
dotnet --version
```

> If you see nothing, go to check the [dotnet sdk installation doc](../../docs/02.Install_dotnet_sdk.md)

## 2. Create a new console app

```shell
# this line will generate a project skeleton for a C# console ap
dotnet new console -n TestApp

```

You should see a new folder `TestApp`. It contains the following files:
- Program.cs: a file to host application logic, you can have multiple class files or packages which contain class files. 
- TestApp.csproj: Project config file  

### 2.1 Adds some logic to your application

Put the below code in `Program.cs`

```csharp
// using System: This imports a namespace(package) `System`, similar to `import sys` in python.
// System: A built-in .NET namespace(package) that contains core classes like Console, String, Math, etc.
using System;

// This defines a class named Test.
// In C#, all code must be inside a class or struct.
class Test
{
    // static: Means this method belongs to the class itself, not to an object instance.
    // void: The method returns nothing.
    // Main: This is the special method that the .NET runtime looks for when the program starts.
    // string[] args: An array of strings representing command-line arguments passed to the program.
    static void Main(string[] args)
    {    
        // Console: A class in the System namespace. As we call using System at the start, we don't need the full path System.Console.WriteLine()
        // WriteLine(): A method that writes a line of text to the terminal (with a newline at the end).
        // "Hello, World!": The string argument for method WriteLine().
        Console.WriteLine("Hello, World!");
    }
}

```

> Unlike Java, the class name(Test) and file name(Program) can be diffrent in C#

### 2.2 TestApp.csproj (Project config) file

This file is a `C# project file` written in **XML** that defines how your .NET project is built. 

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>

```

Let's break down each part of the file you posted:

- **<Project Sdk="Microsoft.NET.Sdk">**: This tells `MSBuild` to use the **.NET SDK-style project system**, introduced 
        in `.NET Core`. `Microsoft.NET.Sdk` is the standard SDK for building console apps, libraries, and more.
- **<PropertyGroup>...</PropertyGroup>**: A group of settings (properties) for the build configuration.
- **<OutputType>Exe</OutputType>**: This tells the compiler to build an `executable program (.exe)`. The alternative would be `Library` for a `.dll`
- <TargetFramework>net9.0</TargetFramework>: This sets the target version of .NET for your app.
- <ImplicitUsings>enable</ImplicitUsings>: When enabled, common namespaces (like `System`, `System.Linq`, etc.) are automatically included — you don't need to write `using System;`.
- <Nullable>enable</Nullable>: Enables nullable reference type warnings and analysis.



> There are other SDKs too, like: `Microsoft.NET.Sdk.Web` (for ASP.NET apps) and `Microsoft.NET.Sdk.Razor` (for Blazor apps).



> Add private string name = null; to Program.cs and rerun the application, you should see nullable warning

## 3 Run the app

```shell
cd TestApp

dotnet run

# you should see hello world in the console
```

### 4. Build an executable for the app

```shell
cd TestApp

# simple build 
# this will create TestApp.dll, .exe, etc in bin/Debug
dotnet build

# build the app in release mode,
# this will create TestApp.dll in bin/Release
dotnet build -c Release

# build the app in release mode, with an a target system specification
dotnet build -c Release -r win-x64 

# --self-contained true will include all required dll in the release folder.
# when you copy the release folder you will have the required dll.
dotnet build -c Release -r win-x64 --self-contained true
```