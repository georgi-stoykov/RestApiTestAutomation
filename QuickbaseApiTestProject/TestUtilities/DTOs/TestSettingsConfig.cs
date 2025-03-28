using System.ComponentModel.DataAnnotations;

namespace QuickbaseApiTestProject.TestUtilities;

public record ApiMode
{
    public const string XML = "XML";
    public const string JSON = "JSON";
}

public class TestSettingsConfig
{
    [Required]
    public string ApiMode { get; set; }
}

