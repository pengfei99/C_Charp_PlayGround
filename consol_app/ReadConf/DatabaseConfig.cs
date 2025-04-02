namespace ReadConf;

public class DatabaseConfig
{
    public string ConnectionString { get; set; }
    public int MaxConnections { get; set; }
    public string[] AllowedUsers { get; set; }
}