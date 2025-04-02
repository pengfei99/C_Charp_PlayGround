namespace ReadConf;

class Program
    {
        static void Main(string[] args)
        {
            // Path to TOML configuration file
            string configPath = "C:\\Users\\PLIU\\Documents\\git\\C_Charp_PlayGround\\consol_app\\ReadConf\\config.toml";
            
            // Create config reader
            ConfigReader reader = new ConfigReader(configPath);

            try
            {
                // Read the configuration
                AppConfiguration config = reader.ReadConfig();

                // Print the configuration
                reader.PrintConfig(config);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process configuration: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }