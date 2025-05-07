# Pass CLI arguments

In this tutorial, we will learn how to pass command line arguments to the main method.

## 1. build project skeleton

```shell
dotnet new console -n GetCliArgs
```

## 2. Add logic

```csharp
using System;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Please provide the correct number of arguments.");
            Console.WriteLine("Example: GetCliArgs.exe -- config.toml verbose log.txt");
            return;
        }
        Console.WriteLine($"Config file path: {args[0]}");
        Console.WriteLine($"App log mode: {args[1]}");
        Console.WriteLine($"Log file path: {args[2]}");
    }
}

```

## 3. Test the app

```shell
# correct inputs
dotnet run -- config.toml verbose log.txt

# bad inputs
dotnet run -- config.toml
```

## 4. Build the app


```shell
# build without .Net dependencies, this requires .Net to run the generated .exe
dotnet build -c Release

# build in self contained mode, the generated .exe works without .Net
dotnet publish -c Release -r win-x64 --self-contained true 

# use trimming to reduce size, but Trimming can sometimes remove necessary code — test thoroughly.
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishTrimmed=true
```

This will generate `Release\net9.0` folder which contains the below files

```text
GetCliArgs.deps.json
GetCliArgs.dll
GetCliArgs.exe
GetCliArgs.pdb
GetCliArgs.runtimeconfig.json
```

## 5. Test the build

```shell
# execute the generated .dll
dotnet GetCliArgs.dll config.toml verbose log.txt

# execute the generated .exe, only works in self contained build
.\GetCliArgs.exe config.toml verbose log.txt
```
