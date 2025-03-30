namespace QuickbaseApiTestProject.Utilities.ConfigDTOs;

public record ApiMode
{
    public const string XML = "XML";
}

public class TestRunConfig
{
    public string ApiMode { get; set; }
    
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string TestTableId { get; set; }
    
    public string AppToken { get; set; }
}

