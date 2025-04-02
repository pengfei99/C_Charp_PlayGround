using Tomlet;
using Tomlet.Models;

namespace ReadConf;

 // Main class to read and process TOML config
    public class ConfigReader
    {
        private string _configFilePath;
        
        public ConfigReader(string configFilePath)
        {
            _configFilePath = configFilePath;
        }
        
        public AppConfiguration ReadConfig()
        {
            try
            {
                // Read the TOML file content
                string tomlContent = File.ReadAllText(_configFilePath);
                
                // Parse the TOML content
                TomlDocument document = TomlParser.ParseFile(tomlContent);
                
                // Convert the TOML document to our configuration class
                AppConfiguration config = TomletMain.To<AppConfiguration>(document);
                
                return config;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Configuration file not found: {_configFilePath}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration: {ex.Message}");
                throw;
            }
        }
        
        // Method to print configuration to console
        public void PrintConfig(AppConfiguration config)
        {
            Console.WriteLine("Application Configuration:");
            Console.WriteLine("-------------------------");
            
            // Print Server section
            Console.WriteLine("\n[Server]");
            Console.WriteLine($"Host: {config.Server.Host}");
            Console.WriteLine($"Port: {config.Server.Port}");
            Console.WriteLine($"EnableSsl: {config.Server.EnableSsl}");
            
            // Print Database section
            Console.WriteLine("\n[Database]");
            Console.WriteLine($"ConnectionString: {config.Database.ConnectionString}");
            Console.WriteLine($"MaxConnections: {config.Database.MaxConnections}");
            
            Console.WriteLine("AllowedUsers:");
            if (config.Database.AllowedUsers != null)
            {
                foreach (var user in config.Database.AllowedUsers)
                {
                    Console.WriteLine($"  - {user}");
                }
            }
            
            // Print Features section
            Console.WriteLine("\n[Features]");
            Console.WriteLine($"EnableLogging: {config.Features.EnableLogging}");
            Console.WriteLine($"LogLevel: {config.Features.LogLevel}");
            Console.WriteLine($"CacheTimeoutMinutes: {config.Features.CacheTimeoutMinutes}");
        }
    }