using System.ComponentModel.DataAnnotations;

namespace QuickbaseApiTestProject.Utilities.ConfigDTOs;

public class ApiSettingsConfig
{
        public const string XmlApiConfig = "XmlApiConfig";
    
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
