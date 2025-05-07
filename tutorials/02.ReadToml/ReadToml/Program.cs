using System;
using System.IO;
using Tomlyn;
using Tomlyn.Model;

class Program
{
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

