# Read toml

In this project, we will write a little program which reads a toml config file and prints the content on the console.

As we will use a third party package to parse the toml file, so we will see how to manage package in .Net projects.


## 1. Set up the project skeleton


```shell
# create a project
dotnet new console -n ReadToml
 
# goto the project folder
cd ReadToml

# install the required package
dotnet add package Tomlyn --version 0.19.0
```
You can notice in the content of the `ReadToml.csproj`, we get an extra section called `<ItemGroup>`. In which, we 
can find the `<PackageReference>` for `Tomlyn.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Tomlyn" Version="0.19.0" />
  </ItemGroup>

</Project>
```

## 2. Add a sample toml file

Below is a toml sample file, which we want to parse

```toml
title = "TOML config example"

[owner]
name = "John Doe"
dob = 1979-05-27T07:32:00Z

[org]
name = "CASD"
location = "Malakoff"
```

## 3. Add a main function to parse the toml file

Add the below code into `Program.cs`

```csharp
using System;
using System.IO;
using Tomlyn;
using Tomlyn.Model;

class Program
{
    static void Main()
    {
        string filePath = "config.toml";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("TOML file not found!");
            return;
        }
        // get the file content as string
        string tomlContent = File.ReadAllText(filePath);
        // convert the string to toml model,
        // if succes, output a var model of type TomlTable
        // if failed, output a var diagnostics
        if (!Toml.TryToModel(tomlContent, out TomlTable model, out var diagnostics))
        {
            Console.WriteLine("Failed to parse TOML:");
            foreach (var diag in diagnostics)
                Console.WriteLine(diag);
            return;
        }

        Console.WriteLine("Parsed TOML content:");
        foreach (var kv in model)
        {
            Console.WriteLine($"{kv.Key}: {kv.Value}");
        }

        // Access nested data block owner
        if (model["owner"] is TomlTable owner)
        {
            Console.WriteLine("\n[owner]");
            foreach (var kv in owner)
            {
                Console.WriteLine($"{kv.Key}: {kv.Value}");
            }
        }
        
        // Access nested data block org
        if (model["org"] is TomlTable org)
        {
            Console.WriteLine("\n[org]");
            foreach (var kv in org)
            {
                Console.WriteLine($"{kv.Key}: {kv.Value}");
            }
        }
    }
}


```

## 4. Run the app and check the result

```shell
dotnet run 
```

## 5. Upgrade the program with user input

Now we want to allow user to enter the path of the toml file. we will add the below line

```csharp
static void Main()
    {
        Console.Write("Enter the path to the TOML file: ");
        string? filePath = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(filePath))
        {
            Console.WriteLine("No file path provided.");
            return;
        }

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }
        ......
```