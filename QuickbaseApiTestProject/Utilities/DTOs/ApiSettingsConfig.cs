using System.ComponentModel.DataAnnotations;

namespace QuickbaseApiTestProject.TestUtilities;

public class ApiSettingsConfig
{
        public const string XmlApiConfig = "XmlApiConfig";
        public const string JsonApiConfig = "JsonApiConfig";
    
        [Required]
        public string BaseApiUrl { get; set; }
        
        [Required]
        public Endpoints Endpoints { get; set; }
}

public class Endpoints
{
        [Required]
        public string Authenticate { get; set; }
        [Required]
        public string Record { get; set; }
}
