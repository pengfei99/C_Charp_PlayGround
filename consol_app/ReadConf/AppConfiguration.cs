namespace ReadConf;

public class AppConfiguration
{
    // Server section
    public ServerConfig Server { get; set; }
        
    // Database section
    public DatabaseConfig Database { get; set; }
        
    // Features section
    public FeaturesConfig Features { get; set; }
        
    public AppConfiguration()
    {
        Server = new ServerConfig();
        Database = new DatabaseConfig();
        Features = new FeaturesConfig();
    }
}