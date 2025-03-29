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
        public string AddRecord { get; set; }
        [Required]
        public string DeleteRecord { get; set; }
        [Required]
        public string PurgeRecords { get; set; }
        [Required]
        public string EditRecord { get; set; }
}
