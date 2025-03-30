using System.ComponentModel.DataAnnotations;

namespace QuickbaseApiTestProject.Utilities.ConfigDTOs;

public class ApiSettingsConfig
{
        public const string XmlApiConfig = "XmlApiConfig";
    
        public string BaseApiUrl { get; set; }
        
        public Endpoints Endpoints { get; set; }
}

public class Endpoints
{
        public string Authenticate { get; set; }
        public string Record { get; set; }
}
