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
